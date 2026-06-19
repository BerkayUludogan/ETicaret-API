using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Domain.Entities.Order;
using ETicaret.Domain.Entities.Payment;
using ETicaret.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Payments.Rules
{
    public class PaymentBusinessRules : IPaymentBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OrderEntity> OrderMustExistForPayment(Guid userId, Guid orderId)
        {
            var order = await _unitOfWork
                .GetReadRepository<OrderEntity>()
                .GetWhere(x =>
                x.Id == orderId &&
                x.UserId == userId &&
                !x.IsDeleted, true
                ).FirstOrDefaultAsync();

            if (order is null)
                throw new BusinessRuleException(PaymentErrors.OrderNotFoundForPayment);

            return order;
        }
        public void OrderMustBePending(OrderEntity order)
        {
            if (order.Status != OrderStatus.Pending)
                throw new BusinessRuleException(PaymentErrors.OrderIsNotPending);
        }
        public async Task OrderMustNotBePaidBefore(Guid orderId)
        {
            var hasSucceededPayment = await _unitOfWork
                .GetReadRepository<PaymentEntity>()
                .GetWhere(x =>
                x.OrderId == orderId &&
                x.Status == PaymentStatus.Succeeded &&
                !x.IsDeleted, false
                ).AnyAsync();

            if (hasSucceededPayment)
                throw new BusinessRuleException(PaymentErrors.OrderAlreadyPaid);
        }
    }
}
