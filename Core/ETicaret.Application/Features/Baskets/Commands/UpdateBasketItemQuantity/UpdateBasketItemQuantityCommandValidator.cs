using FluentValidation;

namespace ETicaret.Application.Features.Baskets.Commands.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandValidator : AbstractValidator<UpdateBasketItemQuantityCommandRequest>
    {
        public UpdateBasketItemQuantityCommandValidator()
        {
            RuleFor(x => x.BasketItemId).NotEmpty();

            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
