using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Orders.Rules;
using ETicaret.Domain.Entities.Catalog;
using ETicaret.Domain.Entities.Order;
using ETicaret.Domain.Enums;
using MediatR;

namespace ETicaret.Application.Features.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommandRequest, CancelOrderCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderBusinessRules _orderBusinessRules;

        public CancelOrderCommandHandler(IUnitOfWork unitOfWork, IOrderBusinessRules orderBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<CancelOrderCommandResponse> Handle(CancelOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var order = await _orderBusinessRules.OrderMustExistWithItems(request.OrderId);
                var oldStatus = order.Status;
                _orderBusinessRules.CompletedOrderStatusCannotBeChanged(order);

                foreach (var item in order.Items)
                {
                    item.Product.StockQuantity += item.Quantity;
                    item.Product.ModifiedDate = DateTime.UtcNow;
                }
                order.Status = OrderStatus.Cancelled;
                order.ModifiedDate = DateTime.UtcNow;

                var history = new OrderStatusHistoryEntity
                {
                    OrderId = order.Id,
                    OldStatus = oldStatus,
                    NewStatus = order.Status,
                    ChangedByUserId = request.ChangedByUserId
                };

                _unitOfWork.GetWriteRepository<ProductEntity>()
                    .UpdateRange(order.Items.Select(x => x.Product).ToList());

                _unitOfWork.GetWriteRepository<OrderEntity>()
                    .Update(order);
                await _unitOfWork.GetWriteRepository<OrderStatusHistoryEntity>()
                    .AddAsync(history);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new CancelOrderCommandResponse
                {
                    OrderId = request.OrderId,
                    Status = order.Status
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
