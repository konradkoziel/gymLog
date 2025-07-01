using gymLog.API.Model.DTO;
using System.Security.Claims;


namespace gymLog.API.Services.Interfaces
{
    public interface IClaimsService
    {
        Result<string> GetUserEmailFromClaims(ClaimsPrincipal principal);
    }
}
