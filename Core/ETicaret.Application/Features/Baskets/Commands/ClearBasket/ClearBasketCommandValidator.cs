using FluentValidation;

namespace ETicaret.Application.Features.Baskets.Commands.ClearBasket
{
    public class ClearBasketCommandValidator : AbstractValidator<ClearBasketCommandRequest>
    {
        public ClearBasketCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
