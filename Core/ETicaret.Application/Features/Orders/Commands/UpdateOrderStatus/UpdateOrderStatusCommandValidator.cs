using ETicaret.Domain.Enums;
using FluentValidation;

namespace ETicaret.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommandRequest>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty();

            RuleFor(x => x.Status)
                .IsInEnum()
                .Must(x => x != OrderStatus.Pending)
                .WithMessage("Sipariş durumu tekrar Pending yapılamaz.");
        }
    }
}
