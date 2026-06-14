using ETicaret.Application.Common.Validation;
using FluentValidation;

namespace ETicaret.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).ProductName();
            RuleFor(x => x.Slug).ProductSlug();
            RuleFor(x => x.Description).ProductDescription();
            RuleFor(x => x.SKU).ProductSku();

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.DiscountPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.DiscountPrice.HasValue)
                .WithMessage("İndirimli fiyat 0'dan küçük olamaz.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stok miktarı 0'dan küçük olamaz.");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Kategori seçimi zorunludur.");
        }
    }
}
