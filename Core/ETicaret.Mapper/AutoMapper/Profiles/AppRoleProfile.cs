using AutoMapper;
using ETicaret.Application.DTOs.Role;
using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Mapper.AutoMapper.Profiles
{
    public class AppRoleProfile : Profile
    {
        public AppRoleProfile()
        {
            CreateMap<AppRoleEntity, AppRoleListDto>();
        }
    }
}
