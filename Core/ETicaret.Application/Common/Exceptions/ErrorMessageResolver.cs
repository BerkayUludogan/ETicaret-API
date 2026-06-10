using ETicaret.Application.Common.Exceptions.Errors;

namespace ETicaret.Application.Common.Exceptions
{
    public static class ErrorMessageResolver
    {
        private static readonly IReadOnlyDictionary<string, string> Messages =
            new Dictionary<string, string>
            {
                [UserErrors.NotFound] = "Kullanıcı bulunamadı",
                [UserErrors.EmailAlreadyExists] = "Bu email zaten kayıtlı",
                [UserErrors.UserNameAlreadyExists] = "Bu kullanıcı adı zaten kayıtlı",
                [UserErrors.PasswordTooShort] = "Şifre çok kısa",

                [AuthErrors.InvalidCredentials] = "Email veya şifre hatalı",
                [AuthErrors.Unauthorized] = "Yetkisiz işlem",

                [CommonErrors.ValidationError] = "Doğrulama hatası oluştu",
                [CommonErrors.UserEmailNotFound] = "Kullanıcı email bilgisi bulunamadı."
            };
        public static string Get(string key)
            => Messages.TryGetValue(key, out var value)
                ? value
                : key;
    }
}
