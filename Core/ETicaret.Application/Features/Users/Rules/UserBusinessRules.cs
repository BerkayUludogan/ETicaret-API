using ETicaret.Application.Shared.Exceptions;
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
            
                
        }

        public Task UserNameMustBeUnique(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
