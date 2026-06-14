using ETicaret.Application.Common.Validation;
using ETicaret.Domain.Entities.Catalog;
using FluentValidation;

namespace ETicaret.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).ProductId();
            RuleFor(x => x.Name).ProductName();
            RuleFor(x => x.Slug).ProductSlug();
            RuleFor(x => x.Description).ProductDescription();
            RuleFor(x => x.SKU).ProductSku();
            RuleFor(x => x.Price).ProductPrice();
            RuleFor(x => x.DiscountPrice).ProductDiscountPrice();
            RuleFor(x => x.StockQuantity).ProductStockQuantity();
            RuleFor(x => x.CategoryId).CategoryId();

        }
    }
}
