using FluentValidation;

namespace ETicaret.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandRequest>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
             .Cascade(CascadeMode.Stop)
             .NotEmpty().WithMessage("Refresh token boş olamaz.")
             .MaximumLength(500).WithMessage("Refresh token geçersiz.");
        }
    }
}
