using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Application.Features.Auth.Rules;
using ETicaret.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommandRequest>
    {
        private readonly UserManager<AppUserEntity> _userManager;
        private readonly IAuthBusinessRules _authBusinessRules;

        public LogoutCommandHandler(UserManager<AppUserEntity> userManager, IAuthBusinessRules authBusinessRules)
        {
            _userManager = userManager;
            _authBusinessRules = authBusinessRules;
        }

        public async Task Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                throw new UnauthorizedException(AuthErrors.Unauthorized);

            user.RefreshToken = null;
            user.RefreshTokenEndDate = null;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new ValidationException(AuthErrors.LogoutFailed);
        }
    }
}
