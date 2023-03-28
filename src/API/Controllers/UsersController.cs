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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Endpoint to get a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<UserByIdResponseDto>), 200)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var response = await _userService.GetUserById(id);
            return Ok(response);
        }

        /// <summary>
        /// Endpoint to register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = "SUPERADMIN")]
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(SuccessResponse<CreateUserResponseDto>), 200)]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto model)
        {
            var response = await _userService.Register(model);
            return Ok(response);
        }
    }
}
