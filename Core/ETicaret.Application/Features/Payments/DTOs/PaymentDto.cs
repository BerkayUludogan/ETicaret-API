namespace ETicaret.Application.Features.Payments.DTOs
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }

        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public string TransactionId { get; set; } = string.Empty;
        public DateTime? PaidDate { get; set; }
        public string? FailedReason { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
