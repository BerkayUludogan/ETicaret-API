using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Features.Auth.Rules
{
    public class AuthBusinessRules : IAuthBusinessRules
    {
        private readonly UserManager<AppUserEntity> _userManager;

        public AuthBusinessRules(UserManager<AppUserEntity> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AppUserEntity> UserMustExistByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new UnauthorizedException(AuthErrors.InvalidCredentials);
            return user;
        }

        public Task UserMustBeActive(AppUserEntity user)
        {
            if (!user.IsActive)
                throw new UnauthorizedException(AuthErrors.Unauthorized);
            return Task.CompletedTask;
        }
         
        public async Task UserPasswordMustBeValid(AppUserEntity user, string password)
        {
            var isValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isValid)
            {
                await _userManager.AccessFailedAsync(user);
                throw new UnauthorizedException(AuthErrors.InvalidCredentials);
            }
            await _userManager.ResetAccessFailedCountAsync(user);
        }

        public async Task UserMustNotBeLockedOut(AppUserEntity user)
        {
            if (await _userManager.IsLockedOutAsync(user))
                throw new UnauthorizedException(AuthErrors.UserLockedOut);
        }
    }
}
