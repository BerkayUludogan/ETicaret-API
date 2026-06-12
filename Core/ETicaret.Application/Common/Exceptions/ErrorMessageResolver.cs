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
                [AuthErrors.UserNotActive] = "Kullanıcı hesabı aktif değil",
                [AuthErrors.EmailNotConfirmed] = "E-posta adresi doğrulanmamış",
                [AuthErrors.RefreshTokenNotSaved] = "Refresh token kaydedilemedi",
                [AuthErrors.InvalidRefreshToken] = "Refresh token geçersiz",
                [AuthErrors.ExpiredRefreshToken] = "Refresh token süresi dolmuş",
                [AuthErrors.UserLockedOut] = "Çok fazla hatalı giriş denemesi yapıldı. Lütfen daha sonra tekrar deneyin.",
                [AuthErrors.LogoutFailed] = "Çıkış işlemi tamamlanmadı.",

                [CommonErrors.ValidationError] = "Doğrulama hatası oluştu",
                [CommonErrors.UserEmailNotFound] = "Kullanıcı email bilgisi bulunamadı.",

                [CategoryErrors.NameAlreadyExists] = "Bu kategori adı zaten kullanılıyor",
                [CategoryErrors.SlugAlreadyExists] = "Bu kategori slug değeri zaten kullanılıyor",
                [CategoryErrors.ParentCategoryNotFound] = "Üst kategori bulunamadı",


            };
        public static string Get(string key)
            => Messages.TryGetValue(key, out var value)
                ? value
                : key;
    }
}
