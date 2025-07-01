using gymLog.API.Model;
using System.Security.Claims;

namespace gymLog.API.Services.Interfaces
{
    public interface ITokenService
    {
        (string Token, DateTime TokenExpires) GenerateAccessToken(User user);
        (string Token, DateTime TokenExpires) GenerateRefreshToken();
        Task<ClaimsPrincipal> GetClaimsPrincipalFromExpiredToken(string token);
    }
}
