using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Application.Features.Auth.Rules
{
    public interface IAuthBusinessRules
    {
        Task<AppUserEntity> UserMustExistByEmail(string email);
        Task UserMustBeActive(AppUserEntity user);
        Task UserPasswordMustBeValid(AppUserEntity user,string password);
        Task UserMustNotBeLockedOut(AppUserEntity user);
    }
}
