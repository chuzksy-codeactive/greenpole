using Application.Contracts;
using Application.DTOs;
using Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        // <summary>Endpoint to login a user</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(SuccessResponse<UserLoginResponseDto>), 200)]
        public async Task<IActionResult> LoginUser(UserLoginDto model)
        {
            var response = await _authService.Login(model);
            return Ok(response);
        }

        /// <summary>
        /// Endpoint to generate a new access and refresh token
        /// </summary>
        /// <param name="mdoel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(SuccessResponse<RefreshTokenResponseDto>), 200)]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto mdoel)
        {
            var response = await _authService.GetRefreshToken(mdoel);
            return Ok(response);
        }

        /// <summary>
        /// Endpoint to initializes password reset
        /// </summary>
        /// <param name="mdoel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(SuccessResponse<object>), 200)]
        public async Task<IActionResult> ForgotPassword(ResetPasswordDto mdoel)
        {
            var response = await _authService.ResetPassword(mdoel);
            return Ok(response);
        }

        /// <summary>
        /// Endpoint to verify token
        /// </summary>
        /// <param name="mdoel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("verify-token")]
        [ProducesResponseType(typeof(SuccessResponse<GetConifrmedTokenUserDto>), 200)]
        public async Task<IActionResult> VerifyToken(VerifyTokenDto mdoel)
        {
            var response = await _authService.ConfirmToken(mdoel);
            return Ok(response);
        }

        /// <summary>
        /// Endpoint to set password
        /// </summary>
        /// <param name="mdoel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("set-password")]
        [ProducesResponseType(typeof(SuccessResponse<GetSetPasswordDto>), 200)]
        public async Task<IActionResult> SetPassword([FromForm] SetPasswordDto mdoel)
        {
            var response = await _authService.SetPassword(mdoel);

            return Ok(response);
        }
    }
}
