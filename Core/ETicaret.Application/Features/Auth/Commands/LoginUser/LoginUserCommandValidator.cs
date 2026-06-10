using ETicaret.Application.Common.Constants.FieldLengths;
using ETicaret.Application.Common.Validation;
using FluentValidation;

namespace ETicaret.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommandRequest>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.UsernameEmail).ValidEmail();
            RuleFor(x => x.Password).ValidPassword(); 
        }
    }
}
