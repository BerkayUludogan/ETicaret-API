using ETicaret.Application.Common.Constants.FieldLengths;
using FluentValidation;

namespace ETicaret.Application.Common.Validation
{ 
    public static class CategoryValidationRules
    {
        public static IRuleBuilderOptions<T, Guid> CategoryId<T>(
            this IRuleBuilderInitial<T, Guid> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Id boş olamaz.");
        }
        public static IRuleBuilderOptions<T, string> CategoryName<T>(
            this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(CatalogFieldLengths.CategoryName)
                .WithMessage($"Kategori adı en fazla {CatalogFieldLengths.CategoryName} karakter olabilir.");
        }
        public static IRuleBuilderOptions<T, string> CategorySlug<T>(
            this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kategori slug değeri boş olamaz.")
                .MaximumLength(CatalogFieldLengths.CategorySlug)
                .WithMessage($"Kategori slug değeri en fazla {CatalogFieldLengths.CategorySlug} karakter olabilir.")
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
                .WithMessage("Kategori slug değeri küçük harf, rakam ve tire içerebilir.");
        }
        public static IRuleBuilderOptions<T, string> CategoryDescription<T>(
           this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Cascade(CascadeMode.Stop)
                .MaximumLength(CatalogFieldLengths.CategoryDescription)
                .WithMessage($"Kategori açıklaması en fazla {CatalogFieldLengths.CategoryDescription} karakter olabilir.");
        }

    }
} 