using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Application.Features.Auth.Rules
{
    public interface IAuthBusinessRules
    {
        Task<AppUserEntity> UserMustExistByEmail(string email);
        Task UserMustBeActive(AppUserEntity user);
        Task UserPasswordMustBeValid(AppUserEntity user, string password);
        Task UserMustNotBeLockedOut(AppUserEntity user);

        Task<AppUserEntity> UserRefreshTokenMustExist(string refreshToken);
        Task RefreshTokenMustNotBeExpired(AppUserEntity user);
    }
}
