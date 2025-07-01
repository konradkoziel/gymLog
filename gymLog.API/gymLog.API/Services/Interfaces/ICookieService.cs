using gymLog.API.Model.DTO.AuthDto;

namespace gymLog.API.Services.Interfaces
{
    public interface ICookieService
    {
        void SetCookies(HttpResponse response, AuthDto authDto);
        void ClearCookies(HttpResponse response);
    }
}
