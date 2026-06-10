using AutoMapper;
using ETicaret.Application.DTOs.Role;
using ETicaret.Application.Features.Users.DTOs;
using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Mapper.AutoMapper.Profiles
{
    public class AppRoleProfile : Profile
    {
        public AppRoleProfile()
        {
            CreateMap<UserResponseDto, AppUserEntity>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
