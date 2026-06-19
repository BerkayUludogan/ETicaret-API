using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Payments.Rules;
using ETicaret.Domain.Entities.Order;
using ETicaret.Domain.Entities.Payment;
using ETicaret.Domain.Enums;
using MediatR;

namespace ETicaret.Application.Features.Payments.Commands.PayOrder
{
    public class PayOrderCommandHandler : IRequestHandler<PayOrderCommandRequest, PayOrderCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentBusinessRules _paymentBusinessRules;

        public PayOrderCommandHandler(IUnitOfWork unitOfWork, IPaymentBusinessRules paymentBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _paymentBusinessRules = paymentBusinessRules;
        }

        public async Task<PayOrderCommandResponse> Handle(PayOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = await _paymentBusinessRules.OrderMustExistForPayment(request.UserId, request.OrderId);

                _paymentBusinessRules.OrderMustBePending(order);
                await _paymentBusinessRules.OrderMustNotBePaidBefore(order.Id);

                var oldStatus = order.Status;

                var payment = new PaymentEntity
                {
                    OrderId = order.Id,
                    UserId = request.UserId,
                    Amount = order.TotalPrice,
                    PaymentMethod = request.PaymentMethod,
                    Status = PaymentStatus.Succeeded,
                    TransactionId = $"MOCK-{Guid.NewGuid():N}",
                    PaidDate = DateTime.UtcNow
                };

                order.Status = OrderStatus.Paid;
                order.ModifiedDate = DateTime.UtcNow;

                var history = new OrderStatusHistoryEntity
                {
                    OrderId = order.Id,
                    OldStatus = oldStatus,
                    NewStatus = order.Status,
                    ChangedByUserId = request.UserId
                };

                await _unitOfWork.GetWriteRepository<PaymentEntity>()
                    .AddAsync(payment);

                await _unitOfWork.GetWriteRepository<OrderStatusHistoryEntity>()
                   .AddAsync(history);

                _unitOfWork.GetWriteRepository<OrderEntity>()
                    .Update(order);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new PayOrderCommandResponse
                {
                    PaymentId = payment.Id,
                    OrderId = order.Id,
                    Amount = payment.Amount,
                    Status = payment.Status,
                    TransactionId = payment.TransactionId
                };
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}