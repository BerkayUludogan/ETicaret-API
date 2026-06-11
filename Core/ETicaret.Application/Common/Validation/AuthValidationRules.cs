using ETicaret.Application.Common.Constants.FieldLengths;
using FluentValidation;

namespace ETicaret.Application.Common.Validation
{
    public static class AuthValidationRules
    {
        public static IRuleBuilderOptions<T, string> ValidPassword<T>(
            this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .Length(AppUserFieldLengths.PasswordMin, AppUserFieldLengths.Password)
                .WithMessage($"Şifre {AppUserFieldLengths.PasswordMin} ile {AppUserFieldLengths.Password} karakter arasında olmalıdır.")
                .Must(value => !string.IsNullOrWhiteSpace(value) && !value.Contains(' '))
                .WithMessage("Şifre boşluk içeremez.");
        }

        public static IRuleBuilderOptions<T, string> ValidEmail<T>(
            this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(CommonFieldLengths.Email)
                .WithMessage($"E-posta adresi en fazla {CommonFieldLengths.Email} karakter olabilir.")
                .Must(email => !string.IsNullOrWhiteSpace(email) && !email.Contains(' '))
                .WithMessage("E-posta adresi boşluk içeremez.");
        }

        public static IRuleBuilderOptions<T, string> ValidUserNameOrEmail<T>(
            this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kullanıcı adı veya e-posta boş olamaz.")
                .MinimumLength(AppUserFieldLengths.UserNameMin)
                .WithMessage($"Kullanıcı adı veya e-posta en az {AppUserFieldLengths.UserNameMin} karakter olmalıdır.")
                .MaximumLength(CommonFieldLengths.Email)
                .WithMessage($"Kullanıcı adı veya e-posta en fazla {CommonFieldLengths.Email} karakter olabilir.")
                .Must(value => !string.IsNullOrWhiteSpace(value) && !value.Contains(' '))
                .WithMessage("Kullanıcı adı veya e-posta boşluk içeremez.");
        }
    }
}