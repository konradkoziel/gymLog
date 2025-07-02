using gymLog.API.Model.DTO;
using gymLog.API.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace gymLog.API.Services
{
    public class ClaimsService : IClaimsService
    {
        public Result<Guid> GetUserIdFromClaims(ClaimsPrincipal principal)
        {
            var sub = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(sub))
            {
                return Result<Guid>.Failure("User ID wasn't found in the token.");
            }
            if (Guid.TryParse(sub, out var id))
            {
                return Result<Guid>.Success(id, "User ID was successfully parsed from string into Guid format.");
            }
            else
            {
                return Result<Guid>.Failure("Invalid format for Guid parse.");
            }
        }

        public Result<string> GetUserEmailFromClaims(ClaimsPrincipal principal)
        {
            var email = principal.FindFirstValue(JwtRegisteredClaimNames.Email);
            return string.IsNullOrEmpty(email)
                         ? Result<string>.Failure("User email was not found in the token.")
                         : Result<string>.Success(email, "User email was found successfully.");
        }
    }
}
