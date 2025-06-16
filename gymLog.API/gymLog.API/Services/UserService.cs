using gymLog.API.Entity;
using gymLog.API.Model;
using gymLog.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Services;

public class UserService(AppDbContext context) : IUserService
{
    private readonly AppDbContext _context = context;

    public async Task<User> AuthenticateAsync(string email, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;
        return user;
    }
}