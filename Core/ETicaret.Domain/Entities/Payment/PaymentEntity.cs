using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Entities.Identity;
using ETicaret.Domain.Entities.Order;
using ETicaret.Domain.Enums;

namespace ETicaret.Domain.Entities.Payment
{
    public class PaymentEntity : BaseEntity
    {
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; } = default!;

        public Guid UserId { get; set; }
        public AppUserEntity User { get; set; } = default!;

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public string TransactionId { get; set; } = string.Empty;
        public DateTime? PaidDate { get; set; }

        public string? FailedReason { get; set; }

    }
}
