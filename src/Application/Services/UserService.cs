using Application.Contracts;
using Application.DTOs;
using Application.Helpers;
using Domain.Entities;
using Domain.Entities.Identities;
using Domain.Enums;
using Infrastructure.Data.AppDbContext;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services
{
    internal class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public UserService(ApplicationDbContext context,
            IMapper mapper,
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<SuccessResponse<UserByIdResponseDto>> GetUserById(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, "User record not found");

            var userResponse = _mapper.Map<UserByIdResponseDto>(user);

            return new SuccessResponse<UserByIdResponseDto>
            {
                Message = "Data retrieved successfully",
                Data = userResponse
            };
        }

        public async Task<SuccessResponse<CreateUserResponseDto>> Register(CreateUserDto model)
        {
            var email = model.Email.Trim().ToLower();
            var emailExist = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (emailExist is not null)
                throw new RestException(HttpStatusCode.BadRequest, "Email address already exists.");

            var user = _mapper.Map<User>(model);
            user.Status = EUserStatus.Pending.ToString();
            user.IsActive = false;
            user.Verified = false;
            user.EmailConfirmed = false;

            var result = await _userManager.CreateAsync(user, "Password@@1");
            if (!result.Succeeded)
                throw new RestException(HttpStatusCode.InternalServerError, "Internal server error");

            await _userManager.AddToRoleAsync(user, ERole.Admin.ToString());

            var token = CustomToken.GenerateRandomString(128);
            var tokenEntity = new Token
            {
                UserId = user.Id,
                Value = token,
                TokenType = ETokenType.InviteUser.ToString()
            };

            await _context.Tokens.AddAsync(tokenEntity);

            await _context.SaveChangesAsync();

            var userResponse = _mapper.Map<CreateUserResponseDto>(user);

            //await SendAdminUserSignUpEmail(user, token);

            return new SuccessResponse<CreateUserResponseDto>
            {
                Data = userResponse
            };
        }
    }
}
