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
                [CategoryErrors.CategoryNotFound] = "Kategori bulunamadı",
                [CategoryErrors.CategoryCannotBeParentOfItself] = "Kategori kendi üst kategorisi olamaz.",

                [ProductErrors.SlugAlreadyExists] = "Bu ürün slug değeri zaten kullanılıyor.",
                [ProductErrors.SkuAlreadyExists] = "Bu ürün SKU değeri zaten kullanılıyor.",
                [ProductErrors.CategoryNotFound] = "Ürünün bağlı olduğu kategori bulunamadı.",
                [ProductErrors.DiscountPriceMustBeLessThanPrice] = "İndirimli fiyat, ürün fiyatından düşük olmalıdır.",
                [ProductErrors.ProductNotFound] = "Ürün bulunamadı",
                [ProductErrors.ProductStockNotEnough] = "Ürün stoğu yeterli değil.",

                [BasketErrors.BasketItemNotFound] = "Sepetteki ürün bulunamadı.",
                [BasketErrors.BasketNotFound] = "Sepet bulunamadı.",

                [OrderErrors.BasketNotFound] = "Sipariş oluşturmak için sepet bulunamadı.",
                [OrderErrors.BasketIsEmpty] = "Sepet boş olduğu için sipariş oluşturulamaz.",
                [OrderErrors.ProductStockNotEnough] = "Ürün stoğu sipariş için yeterli değil.",
                [OrderErrors.OrderNotFound] = "Sipariş bulunamadı.",
                [OrderErrors.CompletedOrderStatusCannotBeChanged] = "Tamamlanmış veya iptal edilmiş siparişin durumu değiştirilemez.",
                [OrderErrors.InvalidOrderStatusTransition] = "Sipariş durumu bu aşamaya geçirilemez.",

                [AddressErrors.AddressNotFound] = "Adres bulunamadı.",

                [PaymentErrors.OrderNotFoundForPayment] = "Ödeme yapılacak sipariş bulunamadı.",
                [PaymentErrors.OrderAlreadyPaid] = "Bu sipariş için ödeme zaten yapılmış.",
                [PaymentErrors.OrderIsNotPending] = "Sadece bekleyen siparişler için ödeme yapılabilir.",

            };
        public static string Get(string key)
            => Messages.TryGetValue(key, out var value)
                ? value
                : key;
    }
}