using AutoMapper;
using gymLog.API.Model.DTO
using gymLog.Model;

namespace gymLog.API.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, AuthDto>()
                .ForMember(dest => dest.AccessToken, opt => opt.Ignore())
                .ForMember(dest => dest.AccessTokenExpires, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshTokenExpires, opt => opt.Ignore());
        }
    }
}
