using ETicaret.Application.Common.Constants.FieldLengths;
using FluentValidation;

namespace ETicaret.Application.Features.Orders.Commands.ShipOrder
{
    public class ShipOrderCommandValidator : AbstractValidator<ShipOrderCommandRequest>
    {
        public ShipOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty();

            RuleFor(x => x.CargoCompany)
                .NotEmpty()
                .MaximumLength(OrderFieldLengths.CargoCompany);

            RuleFor(x => x.TrackingNumber)
                .NotEmpty()
                .MaximumLength(OrderFieldLengths.TrackingNumber);
        }
    }
}
