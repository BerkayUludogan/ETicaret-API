using FluentValidation;

namespace ETicaret.Application.Features.Payments.Commands.PayOrder
{
    public class PayOrderCommandValidator : AbstractValidator<PayOrderCommandRequest>
    {
        public PayOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
               .NotEmpty();

            RuleFor(x => x.PaymentMethod)
                .IsInEnum();
        }
    }
}
