using gymLog.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gymLog.API.Services.interfaces
{
    public interface IUserService : IBasicCrudService<User>
    {
        Task<User> AuthenticateAsync(string email, string password);
    }
}