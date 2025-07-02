using gymLog.API.Model.DTO.AuthDto;
using gymLog.API.Services.Interfaces;

namespace gymLog.API.Services
{
    public class CookieService : ICookieService
    {
        private CookieOptions GetBaseOptions(DateTimeOffset expiresAt) => new()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = expiresAt
        };

        public void SetCookies(HttpResponse response, AuthDto authDto)
        {
            response.Cookies.Append("accessToken", authDto.AccessToken, GetBaseOptions(authDto.AccessTokenExpiresAt));
            response.Cookies.Append("refreshToken", authDto.RefreshToken, GetBaseOptions(authDto.RefreshTokenExpiresAt));
        }

        public void ClearCookies(HttpResponse response)
        {
            response.Cookies.Delete("accessToken");
            response.Cookies.Delete("refreshToken");
        }
    }
}
