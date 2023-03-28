using Application.Contracts;
using Application.DTOs;
using Application.Helpers;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data.AppDbContext;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Entity;
using System.Net;

namespace Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IJwtAuthenticationManager _jwtAuthManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthService(IJwtAuthenticationManager jwtAuthManager,
            ApplicationDbContext context,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _jwtAuthManager = jwtAuthManager;
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<SuccessResponse<GetConifrmedTokenUserDto>> ConfirmToken(VerifyTokenDto model)
        {
            var token = await _context.Tokens.FirstOrDefaultAsync(x => x.Value == model.Token);
            if (token == null)
                throw new RestException(HttpStatusCode.NotFound, "The token is invalid or has expired");

            if (DateTime.Now >= token.ExpiresAt)
            {
                _context.Tokens.Remove(token);
                await _context.SaveChangesAsync();

                throw new RestException(HttpStatusCode.BadRequest, "Token is expired");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == token.UserId);
            if (user == null)
                throw new RestException(HttpStatusCode.BadRequest, "Invalid Token");

            if (token.TokenType == ETokenType.InviteUser.ToString() &&
                (token.ExpiresAt - DateTime.Now) <= TimeSpan.FromMinutes(30))
            {
                token.ExpiresAt = token.ExpiresAt.AddMinutes(30);
                _context.Tokens.Update(token);
                await _context.SaveChangesAsync();
            }

            return new SuccessResponse<GetConifrmedTokenUserDto>
            {
                Message = "Token confirmed successfully",
                Data = new GetConifrmedTokenUserDto
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            };
        }

        public async Task<SuccessResponse<RefreshTokenResponseDto>> GetRefreshToken(RefreshTokenDto model)
        {
            var userId = _jwtAuthManager.GetUserIdFromAccessToken(model.AccessToken);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, "User not found");

            var isRefreshTokenValid = _jwtAuthManager.ValidateRefreshToken(model.RefreshToken);
            if (!isRefreshTokenValid)
                throw new RestException(HttpStatusCode.NotFound, "Invalid token");

            var roles = await _userManager.GetRolesAsync(user);
            var tokenResponse = _jwtAuthManager.Authenticate(user, roles);

            var newRefreshToken = _jwtAuthManager.GenerateRefreshToken(user.Id);

            var tokenViewModel = new RefreshTokenResponseDto
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = newRefreshToken,
                ExpiresIn = tokenResponse.ExpiresIn
            };

            return new SuccessResponse<RefreshTokenResponseDto>
            {
                Message = "Data retrieved successfully",
                Data = tokenViewModel
            };
        }

        public async Task<SuccessResponse<UserLoginResponseDto>> Login(UserLoginDto model)
        {
            var email = model.Email.Trim().ToLower();
            var user = await _userManager.FindByEmailAsync(email);
            var authenticated = await ValidateUser(user, model.Password);

            if (!authenticated)
                throw new RestException(HttpStatusCode.Unauthorized, "Wrong Email or Password");

            if (!user.Verified || !user.IsActive)
                throw new RestException(HttpStatusCode.Unauthorized, "User is inactive");

            user.LastLogin = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            var userActivity = new UserActivity
            {
                EventType = "User Login",
                UserId = user.Id,
                ObjectClass = "USER",
                Details = "logged in",
                ObjectId = user.Id
            };

            var roles = await _userManager.GetRolesAsync(user);

            var tokenResponse = _jwtAuthManager.Authenticate(user, roles);

            return new SuccessResponse<UserLoginResponseDto>
            {
                Message = "Login successful",
                Data = new UserLoginResponseDto
                {
                    AccessToken = tokenResponse.AccessToken,
                    ExpiresIn = tokenResponse.ExpiresIn,
                    RefreshToken = _jwtAuthManager.GenerateRefreshToken(user.Id),
                }
            };
        }

        public async Task<SuccessResponse<object>> ResetPassword(ResetPasswordDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, "User not found");

            var token = CustomToken.GenerateRandomString(128);
            var tokenEntity = new Token
            {
                UserId = user.Id,
                TokenType = ETokenType.ResetPassword.ToString(),
                Value = token
            };
            await _context.Tokens.AddAsync(tokenEntity);
            await _context.SaveChangesAsync();

            //string emailLink = $"{_configuration["CLIENT_URL"]}/reset-password?token={token}";
            //var message = _emailManager.GetResetPasswordEmailTemplate(emailLink, user.Email);
            //string subject = "Reset Password";

            //await _notificationService.SendEmail(new EmailRequestDto
            //{
            //    Subject = subject,
            //    Body = message,
            //    To = user.Email,
            //    Sender = "messaging@stanbicibtc.com"
            //});

            return new SuccessResponse<object>
            {
                Message = "Password reset successfully",
            };
        }

        public async Task<SuccessResponse<GetSetPasswordDto>> SetPassword(SetPasswordDto model)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var token = await _context.Tokens.FirstOrDefaultAsync(x => x.Value == model.Token);
            if (token == null)
                throw new RestException(HttpStatusCode.NotFound, "The token is invalid or has expired");

            var isValid = CustomToken.IsTokenValid(token);
            if (!isValid)
                throw new RestException(HttpStatusCode.NotFound, "Token is invalid");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == token.UserId);
            if (user.Email != model.Email)
                throw new RestException(HttpStatusCode.NotFound, "Token is invalid");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            user.UpdatedAt = DateTime.UtcNow;

            if (token.TokenType == ETokenType.InviteUser.ToString())
            {
                user.IsActive = true;
                user.Status = EUserStatus.Active.ToString();
                user.EmailConfirmed = true;
                user.Verified = true;
            }
            _context.Users.Update(user);

            _context.Tokens.Remove(token);
            await _context.SaveChangesAsync();

            return new SuccessResponse<GetSetPasswordDto>
            {
                Message = "Password set successfully",
                Data = _mapper.Map<GetSetPasswordDto>(user)
            };
        }

        private async Task<bool> ValidateUser(User user, string password)
        {
            var result = (user != null && await _userManager.CheckPasswordAsync(user, password));
            if (!result)
                return false;

            if (user != null && !user.Verified)
            {
                return false;
            }

            return result;
        }
    }
}
