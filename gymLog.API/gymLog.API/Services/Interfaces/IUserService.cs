using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.UserDto;

namespace gymLog.API.Services.Interfaces
{
    public interface IUserService 
    {
        Task<Result<UserDto>> GetCurrentUser(Guid userId);
    }
}