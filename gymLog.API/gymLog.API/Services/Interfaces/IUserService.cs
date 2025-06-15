using gymLog.Model;

namespace gymLog.API.Services.interfaces
{
    public interface IUserService : IBasicCrudService<User>
    {
        Task<User> AuthenticateAsync(string email, string password);
    }
}