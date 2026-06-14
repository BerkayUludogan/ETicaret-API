using ETicaret.Application.Common.Constants.FieldLengths;
using FluentValidation;

namespace ETicaret.Application.Common.Validation
{
    public static class ProductValidationRules
    {
        public static IRuleBuilderOptions<T, string> ProductName<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ürün adı boş olamaz.")
                .MaximumLength(CatalogFieldLengths.ProductName)
                .WithMessage($"Ürün adı en fazla {CatalogFieldLengths.ProductName} karakter olabilir.");
        }
        public static IRuleBuilderOptions<T, string> ProductSlug<T>(
           this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ürün slug değeri boş olamaz.")
                .MaximumLength(CatalogFieldLengths.ProductSlug)
                .WithMessage($"Ürün slug değeri en fazla {CatalogFieldLengths.ProductSlug} karakter olabilir.")
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
                .WithMessage("Ürün slug değeri küçük harf, rakam ve tire içerebilir.");
        }

        public static IRuleBuilderOptions<T, string> ProductDescription<T>(
            this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(CatalogFieldLengths.ProductDescription)
                .WithMessage($"Ürün açıklaması en fazla {CatalogFieldLengths.ProductDescription} karakter olabilir.");
        }

        public static IRuleBuilderOptions<T, string> ProductSku<T>(
            this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ürün SKU değeri boş olamaz.")
                .MaximumLength(CatalogFieldLengths.ProductSku)
                .WithMessage($"Ürün SKU değeri en fazla {CatalogFieldLengths.ProductSku} karakter olabilir.");
        }
    }
}

