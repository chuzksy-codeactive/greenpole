using Application.DTOs;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IJwtAuthenticationManager
    {
        TokenResponseDto Authenticate(User user, IList<string> roles);
        Guid GetUserIdFromAccessToken(string accessToken);
        string GenerateRefreshToken(Guid userId);
        bool ValidateRefreshToken(string refreshToken);
    }
}
