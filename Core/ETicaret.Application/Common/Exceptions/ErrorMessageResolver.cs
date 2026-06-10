using ETicaret.Application.Shared.Exceptions.Errors;

namespace ETicaret.Application.Shared.Exceptions
{
    public static class ErrorMessageResolver
    {
        private static readonly IReadOnlyDictionary<string, string> Messages =
            new Dictionary<string, string>
            {
                [UserErrors.NotFound] = "Kullanıcı bulunamadı",
                [UserErrors.EmailAlreadyExists] = "Bu email zaten kayıtlı",
                [UserErrors.PasswordTooShort] = "Şifre çok kısa",

                [AuthErrors.InvalidCredentials] = "Email veya şifre hatalı",
                [AuthErrors.Unauthorized] = "Yetkisiz işlem",

                [CommonErrors.ValidationError] = "Doğrulama hatası oluştu"
            };
        public static string Get(string key)
            => Messages.TryGetValue(key, out var value)
                ? value 
                : key;
    }
}
