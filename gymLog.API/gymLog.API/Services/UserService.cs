using AutoMapper;
using gymLog.Entity;
using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.UserDto;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public UserService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Result<UserDto>> GetCurrentUser(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return Result<UserDto>.Failure("Not found");
            }
            return Result<UserDto>.Success(_mapper.Map<UserDto>(user));
        }
    }
}







