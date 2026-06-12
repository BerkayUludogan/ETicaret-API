using ETicaret.Application.Common.Constants.FieldLengths;
using FluentValidation;

namespace ETicaret.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandRequest>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Kategori adı boş olamaz.")
               .MaximumLength(CatalogFieldLengths.CategoryName)
               .WithMessage($"Kategori adı en fazla {CatalogFieldLengths.CategoryName} karakter olabilir.");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Kategori slug değeri boş olamaz.")
                .MaximumLength(CatalogFieldLengths.CategorySlug)
                .WithMessage($"Kategori slug değeri en fazla {CatalogFieldLengths.CategorySlug} karakter olabilir.")
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
                .WithMessage("Kategori slug değeri küçük harf, rakam ve tire içerebilir.");

            RuleFor(x => x.Description)
                .MaximumLength(CatalogFieldLengths.CategoryDescription)
                .WithMessage($"Kategori açıklaması en fazla {CatalogFieldLengths.CategoryDescription} karakter olabilir.");
        }
    }
}
