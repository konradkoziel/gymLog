using gymLog.API.Model.DTO;
using gymLog.API.Services.Interfaces;
using System.Security.Claims;

namespace gymLog.API.Services
{
    public class ClaimsService : IClaimsService
    {
        public Result<string> GetUserEmailFromClaims(ClaimsPrincipal principal)
        {
            var email = principal.FindFirstValue(ClaimTypes.Name);
            return string.IsNullOrEmpty(email)
                         ? Result<string>.Failure("User email wasn't found in the token.")
                         : Result<string>.Success(email, "User email was found successfully.");
        }
    }
}
