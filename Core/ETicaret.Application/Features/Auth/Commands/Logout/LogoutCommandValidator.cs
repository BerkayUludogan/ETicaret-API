using ETicaret.Application.Common.Validation;
using FluentValidation;

namespace ETicaret.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandValidator : AbstractValidator<LogoutCommandRequest>
    {
        public LogoutCommandValidator()
        {
            RuleFor(x => x.RefreshToken).ValidRefreshToken();
        }
    }
}
