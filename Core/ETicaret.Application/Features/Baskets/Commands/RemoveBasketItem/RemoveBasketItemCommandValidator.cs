using FluentValidation;

namespace ETicaret.Application.Features.Baskets.Commands.RemoveBasketItem
{
    public class RemoveBasketItemCommandValidator : AbstractValidator<RemoveBasketItemCommandRequest>
    {
        public RemoveBasketItemCommandValidator()
        {
            RuleFor(x => x.BasketItemId).NotEmpty();
        }
    }
}
