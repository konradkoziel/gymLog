using gymLog.API.Model;

namespace gymLog.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string email, string password);
    }
}