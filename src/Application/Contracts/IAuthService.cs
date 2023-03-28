using Application.DTOs;
using Application.Helpers;

namespace Application.Contracts
{
    public interface IAuthService
    {
        Task<SuccessResponse<UserLoginResponseDto>> Login(UserLoginDto model);
        Task<SuccessResponse<RefreshTokenResponseDto>> GetRefreshToken(RefreshTokenDto model);
        Task<SuccessResponse<GetSetPasswordDto>> SetPassword(SetPasswordDto model);
        Task<SuccessResponse<object>> ResetPassword(ResetPasswordDto model);
        Task<SuccessResponse<GetConifrmedTokenUserDto>> ConfirmToken(VerifyTokenDto model);
    }
}
