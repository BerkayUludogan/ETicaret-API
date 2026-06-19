namespace ETicaret.Application.Common.Exceptions.Errors
{
    public static class PaymentErrors
    {
        public const string OrderNotFoundForPayment = "Payment.OrderNotFoundForPayment";
        public const string OrderAlreadyPaid = "Payment.OrderAlreadyPaid";
        public const string OrderIsNotPending = "Payment.OrderIsNotPending";
        public const string PaymentNotFound = "Payment.PaymentNotFound";
    }
}