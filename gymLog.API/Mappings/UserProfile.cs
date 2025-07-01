using AutoMapper;
using gymLog.API.Model.DTO;
using gymLog.Model;

namespace gymLog.API.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDto>();
        }
    }
}
