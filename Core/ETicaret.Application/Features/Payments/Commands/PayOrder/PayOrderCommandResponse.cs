using ETicaret.Domain.Enums;

namespace ETicaret.Application.Features.Payments.Commands.PayOrder
{
    public class PayOrderCommandResponse
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string TransactionId { get; set; } = string.Empty;
    }
}