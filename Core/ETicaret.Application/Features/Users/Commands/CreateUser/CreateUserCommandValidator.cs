using ETicaret.Application.Common.Constants.FieldLengths;
using FluentValidation;

namespace ETicaret.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .Length(AppUserFieldLengths.UserNameMin, AppUserFieldLengths.UserName)
                .WithMessage($"Kullanıcı adı {AppUserFieldLengths.UserNameMin} ile{AppUserFieldLengths.UserName} karakter arasında olmalıdır.")
                .Matches(@"^[A-Za-z][A-Za-z0-9_]*$")
                .WithMessage("Kullanıcı adı harf ile başlamalı ve yalnızca harf, rakam veya alt çizgi (_) içerebilir.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(CommonFieldLengths.Email).WithMessage($"E-posta adresi en fazla {CommonFieldLengths.Email} karakter olabilir.")
                .Must(email => string.IsNullOrWhiteSpace(email) || !email.Contains(" ")).WithMessage("E-posta adresi boşluk içeremez.");

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz")
                .Length(CommonFieldLengths.PhoneNumberMin, CommonFieldLengths.PhoneNumber).WithMessage($"Telefon numarası {CommonFieldLengths.PhoneNumberMin} ile {CommonFieldLengths.PhoneNumber} arasında olmalıdır.")
                .Matches(@"^\+?[0-9]+$").WithMessage("Telefon numarası yalnızca rakamlardan oluşmalı ve isteğe bağlı olarak '+' ile başlayabilir.");

            

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .Length(AppUserFieldLengths.PasswordMin, AppUserFieldLengths.Password)
                    .WithMessage($"Şifre en az {AppUserFieldLengths.PasswordMin} ve en fazla {AppUserFieldLengths.Password} karakter olmalıdır.")
                .Must(p => !p.Contains(" ")).WithMessage("Şifre boşluk içeremez.")
              //  .Matches(@"[a-z]").WithMessage("En az bir küçük harf içermelidir.")
               // .Matches(@"[A-Z]").WithMessage("En az bir büyük harf içermelidir.")
                .Matches(@"\d").WithMessage("En az bir rakam içermelidir.")
               // .Matches(@"[\W_]").WithMessage("En az bir özel karakter içermelidir.")
                .Matches(@"^\S+$").WithMessage("Şifre boşluk karakteri içeremez.");
        }
    }
}
