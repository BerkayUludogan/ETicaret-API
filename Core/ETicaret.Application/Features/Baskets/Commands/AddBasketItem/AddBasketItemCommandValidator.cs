using ETicaret.Application.Features.Baskets.DTOs;
using FluentValidation;

namespace ETicaret.Application.Features.Baskets.Commands.AddBasketItem
{
    public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommandRequest>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
