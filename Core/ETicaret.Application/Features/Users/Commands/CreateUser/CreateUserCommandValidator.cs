using ETicaret.Application.Common.Constants.FieldLengths;
using ETicaret.Application.Common.Validation;
using FluentValidation;

namespace ETicaret.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).ValidEmail();
            RuleFor(x => x.Password).ValidPassword();
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .Length(AppUserFieldLengths.UserNameMin, AppUserFieldLengths.UserName)
                .WithMessage($"Kullanıcı adı {AppUserFieldLengths.UserNameMin} ile{AppUserFieldLengths.UserName} karakter arasında olmalıdır.")
                .Matches(@"^[A-Za-z][A-Za-z0-9_]*$")
                .WithMessage("Kullanıcı adı harf ile başlamalı ve yalnızca harf, rakam veya alt çizgi (_) içerebilir."); 

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz")
                .Length(CommonFieldLengths.PhoneNumberMin, CommonFieldLengths.PhoneNumber).WithMessage($"Telefon numarası {CommonFieldLengths.PhoneNumberMin} ile {CommonFieldLengths.PhoneNumber} arasında olmalıdır.")
                .Matches(@"^\+?[0-9]+$").WithMessage("Telefon numarası yalnızca rakamlardan oluşmalı ve isteğe bağlı olarak '+' ile başlayabilir."); 
        }
    }
}
