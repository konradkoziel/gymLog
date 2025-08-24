using gymLog.API.Entity;
using gymLog.API.Model;
using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.AuthDto;
using gymLog.API.Model.enums;
using gymLog.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<User>> Login(LoginDto login)
        {
            var user = await GetUserByEmail(login.Email);

            if (user.Data == null)
            {
                return Result<User>.Failure("User doesn't exists.",  ErrorCode.UserNotFound);
            }
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user.Data, user.Data.PasswordHash, login.Password);
            if (result == PasswordVerificationResult.Success)
            {
                return Result<User>.Success(user.Data);
            }
            return Result<User>.Failure("Wrong password.", ErrorCode.WrongPassword);

        }

        public async Task<Result<User>> Register(RegisterDto registerDto)
        {
            var user = await GetUserByEmail(registerDto.Email);

            if (user.Data == null)
            {
                var passwordHasher = new PasswordHasher<User>();
                var newUser = new User
                {
                    Email = registerDto.Email,
                };
                string hashedPassword = passwordHasher.HashPassword(newUser, registerDto.Password);
                newUser.PasswordHash = hashedPassword;
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return Result<User>.Success(newUser);
            }
            else
            {
                return Result<User>.Failure("Email already used.", ErrorCode.EmailAlreadyUsed);
            }
        }

        public async Task<Result<User>> GetUserByEmail(string email)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return Result<User>.Failure("User not found.", ErrorCode.UserNotFound);
            }
            else
            {
                return Result<User>.Success(user);
            }
        }
    }
}
