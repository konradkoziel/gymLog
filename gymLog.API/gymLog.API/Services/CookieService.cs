using gymLog.API.Model.DTO.AuthDto;
using gymLog.API.Services.Interfaces;

namespace gymLog.API.Services
{
    public class CookieService: ICookieService
    {
        public void SetCookies(HttpResponse response, AuthDto authDto)
        {
            response.Cookies.Append("accessToken", authDto.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = authDto.AccessTokenExpires
            });

            response.Cookies.Append("refreshToken", authDto.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = authDto.RefreshTokenExpires
            });
        }
        public void ClearCookies(HttpResponse response)
        {
            response.Cookies.Delete("accessToken");
            response.Cookies.Delete("refreshToken");
        }
    }
}

