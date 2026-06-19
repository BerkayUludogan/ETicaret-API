using ETicaret.Domain.Entities.Order;

namespace ETicaret.Application.Features.Payments.Rules
{
    public interface IPaymentBusinessRules
    {
        Task<OrderEntity> OrderMustExistForPayment(Guid userId, Guid orderId);
        void OrderMustBePending(OrderEntity order);
        Task OrderMustNotBePaidBefore(Guid orderId);
    }
}
