using Application.DTOs;
using Application.Helpers;

namespace Application.Contracts
{
    public interface IUserService
    {
        public Task<SuccessResponse<CreateUserResponseDto>> Register(CreateUserDto model);
        Task<SuccessResponse<UserByIdResponseDto>> GetUserById(Guid userId);
    }
}
