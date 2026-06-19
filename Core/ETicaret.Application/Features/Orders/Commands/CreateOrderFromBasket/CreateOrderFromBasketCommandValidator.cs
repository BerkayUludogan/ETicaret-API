using ETicaret.Application.Common.Constants.FieldLengths;
using FluentValidation;

namespace ETicaret.Application.Features.Orders.Commands.CreateOrderFromBasket
{
    public class CreateOrderFromBasketCommandValidator : AbstractValidator<CreateOrderFromBasketCommandRequest>
    {
        public CreateOrderFromBasketCommandValidator()
        {
            RuleFor(x => x.ShippingAddress)
                .NotEmpty()
                .MaximumLength(OrderFieldLengths.ShippingAddress);
        }
    }
}