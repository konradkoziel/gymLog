using gymLog.API.Model.DTO;
using System.Security.Claims;


namespace gymLog.API.Services.Interfaces
{
    public interface IClaimsService
    {
        Result<Guid> GetUserIdFromClaims(ClaimsPrincipal principal);
        Result<string> GetUserEmailFromClaims(ClaimsPrincipal principal);
    }
}
