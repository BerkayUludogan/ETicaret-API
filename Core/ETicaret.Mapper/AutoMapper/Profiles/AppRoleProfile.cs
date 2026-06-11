using AutoMapper;
using ETicaret.Application.Features.Users.Commands.CreateUser;  
using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Mapper.AutoMapper.Profiles
{
    public class AppRoleProfile : Profile
    {
        public AppRoleProfile()
        {
            CreateMap<CreateUserCommandRequest, AppUserEntity>()
    .ForMember(dest => dest.Id, opt => opt.Ignore())
    .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
    .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
    .ForMember(dest => dest.RefreshTokenEndDate, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
    .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
    .ForMember(dest => dest.IsActive, opt => opt.Ignore())
    .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
    .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
    .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
    .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
    .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
    .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
    .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
    .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
    .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
    .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore());
        }
    }
}
