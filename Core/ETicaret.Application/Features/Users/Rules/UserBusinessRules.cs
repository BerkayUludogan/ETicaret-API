using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Features.Users.Rules
{
    public class UserBusinessRules : IUserBusinessRules
    {
        private readonly UserManager<AppUserEntity> _userManager;

        public UserBusinessRules(UserManager<AppUserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task UserEmailMustBeUnique(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
                throw new BusinessRuleException(UserErrors.EmailAlreadyExists);
                
        }

        public async Task UserNameMustBeUnique(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user is not null)
                throw new BusinessRuleException(UserErrors.UserNameAlreadyExists);
        }
    }
}
