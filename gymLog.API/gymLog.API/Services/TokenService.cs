using gymLog.API.Services.Interfaces;
using gymLog.API.Model;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace gymLog.API.Services
{
    public class TokenService(IConfiguration configuration): ITokenService
    {
        public (string Token, DateTime TokenExpires) GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is missing.")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["Jwt:ExpireMinutes"] ?? throw new InvalidOperationException("JWT ExpireMinutes is missing.")));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = expires,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
            };

            var token = new JsonWebTokenHandler().CreateToken(tokenDescriptor);

            return (token, expires);
        }
        public (string Token, DateTime TokenExpires) GenerateRefreshToken()
        {
            var randomBytes = new byte[32];

            using (var randomNumber = RandomNumberGenerator.Create())
            {
                randomNumber.GetBytes(randomBytes);
                return (Convert.ToBase64String(randomBytes), DateTime.UtcNow.AddDays(7));
            }
        }
        public async Task<ClaimsPrincipal> GetClaimsPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is missing."));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var tokenHandler = new JsonWebTokenHandler();

            var validationResult = await tokenHandler.ValidateTokenAsync(token, tokenValidationParameters);

            if (!validationResult.IsValid)
            {
                throw new SecurityTokenException("Token is invalid.");
            }

            return new ClaimsPrincipal(validationResult.ClaimsIdentity);
        }
    }
}
