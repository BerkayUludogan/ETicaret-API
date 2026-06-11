using ETicaret.Application.Common.Validation;
using FluentValidation;

namespace ETicaret.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandRequest>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken).ValidRefreshToken();
        }
    }
}
