using ETicaret.Application.Common.Validation;
using FluentValidation;

namespace ETicaret.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.CategoryId).CategoryId();
            RuleFor(x => x.Name).ProductName();
            RuleFor(x => x.Slug).ProductSlug();
            RuleFor(x => x.Description).ProductDescription();
            RuleFor(x => x.SKU).ProductSku();
            RuleFor(x => x.Price).ProductPrice();
            RuleFor(x => x.DiscountPrice).ProductDiscountPrice();
            RuleFor(x => x.StockQuantity).ProductStockQuantity();
        }
    }
}
