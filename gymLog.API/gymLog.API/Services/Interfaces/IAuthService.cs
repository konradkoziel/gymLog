using gymLog.API.Model;
using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.AuthDto;

namespace gymLog.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<User>> Login(LoginDto loginModel);
        Task<Result<User>> Register(RegisterDto registerDto);
        Task<Result<User>> GetUserByEmail(string email);

    }
}
