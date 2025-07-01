using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.AuthDto;
using gymLog.API.Model.DTO.TokenDto;

namespace gymLog.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthDto>> Login(LoginDto loginDto);
        Task<Result<AuthDto>> Register(RegisterDto registerDto);
        Task<Result<AuthDto>> RefreshToken(TokenDto tokenDto);
        Task<Result<bool>> RevokeToken(string email);
    }
}
