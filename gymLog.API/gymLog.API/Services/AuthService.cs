using AutoMapper;
using gymLog.API.Entity;
using gymLog.API.Model;
using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.AuthDto;
using gymLog.API.Model.DTO.TokenDto;
using gymLog.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace gymLog.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IClaimsService _claimsService;

        public AuthService(IMapper mapper, AppDbContext context, ITokenService tokenService, IClaimsService claimsService)
        {
            _mapper = mapper;
            _context = context;
            _tokenService = tokenService;
            _claimsService = claimsService;
        }

        public async Task<Result<AuthDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Result<AuthDto>.Failure("Invalid credentials");
            }
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            _context.RefreshTokens.Add(new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = refreshToken.Token,
                ExpiresAt = refreshToken.TokenExpires,
                IsRevoked = false,
                IsUsed = false,
                UserId = user.Id,
            });
            await _context.SaveChangesAsync();
            var authDto = _mapper.Map<AuthDto>(user);
            authDto.AccessToken = accessToken.Token;
            authDto.AccessTokenExpiresAt = accessToken.TokenExpires;
            authDto.RefreshToken = refreshToken.Token;
            authDto.RefreshTokenExpiresAt = refreshToken.TokenExpires;
            return Result<AuthDto>.Success(authDto);
        }
        public async Task<Result<AuthDto>> Register(RegisterDto registerDto)
        {
            var user = await _context.Users.AnyAsync(u => u.Email == registerDto.Email);
            if (user)
            {
               return Result<AuthDto>.Failure("User already exists");
            }
            var newUser = _mapper.Map<User>(registerDto);
            newUser.Id = Guid.NewGuid();
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            newUser.CreatedAt = DateTime.UtcNow;
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return Result<AuthDto>.Success(_mapper.Map<AuthDto>(newUser),"User registered successfully");
        }
        public async Task<Result<AuthDto>> RefreshToken(TokenDto tokenDto)
        {
            var principal = await _tokenService.GetClaimsPrincipalFromExpiredToken(tokenDto.AccessToken ?? "");
            var email = _claimsService.GetUserEmailFromClaims(principal);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email.Data);
            var refreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == tokenDto.RefreshToken);
            if (email == null || user == null || refreshToken == null || refreshToken.UserId != user.Id || refreshToken.IsRevoked || refreshToken.IsUsed || refreshToken.ExpiresAt <= DateTime.UtcNow)
            {
                return Result<AuthDto>.Failure("Invalid client request");
            }
            refreshToken.IsUsed = true;
            _context.RefreshTokens.Update(refreshToken);
            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            _context.RefreshTokens.Add(new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = newRefreshToken.Token,
                ExpiresAt = newRefreshToken.TokenExpires,
                IsRevoked = false,
                IsUsed = false,
                UserId = user.Id,
            });
            await _context.SaveChangesAsync();
            var authDto = _mapper.Map<AuthDto>(user);
            authDto.AccessToken = newAccessToken.Token;
            authDto.AccessTokenExpiresAt = newAccessToken.TokenExpires;
            authDto.RefreshToken = newRefreshToken.Token;
            authDto.RefreshTokenExpiresAt = newRefreshToken.TokenExpires;
            return Result<AuthDto>.Success(authDto);
        }
        public async Task<Result<bool>> RevokeToken(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return Result<bool>.Failure("Not found");
            }
            var refreshTokens = await _context.RefreshTokens.Where(rt => rt.UserId == user.Id && !rt.IsUsed && !rt.IsRevoked).ToListAsync();
            foreach(var rt in refreshTokens)
            {
                rt.IsRevoked = true;
            }
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
