using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Orders.Rules;
using ETicaret.Domain.Entities.Order;
using ETicaret.Domain.Enums;
using MediatR;

namespace ETicaret.Application.Features.Orders.Commands.ShipOrder
{
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommandRequest, ShipOrderCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderBusinessRules _orderBusinessRules;

        public ShipOrderCommandHandler(IUnitOfWork unitOfWork, IOrderBusinessRules orderBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<ShipOrderCommandResponse> Handle(ShipOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderBusinessRules.OrderMustExist(request.OrderId);

            _orderBusinessRules.OrderMustBeShippable(order);

            var oldStatus = order.Status;

            order.Status = OrderStatus.Shipped;
            order.CargoCompany = request.CargoCompany;
            order.TrackingNumber = request.TrackingNumber;
            order.ShippedDate = DateTime.UtcNow;
            order.ModifiedDate = DateTime.UtcNow;

            var history = new OrderStatusHistoryEntity
            {
                OrderId = order.Id,
                OldStatus = oldStatus,
                NewStatus = order.Status,
                ChangedByUserId = request.ChangedByUserId
            };

            _unitOfWork.GetWriteRepository<OrderEntity>()
                .Update(order);

            await _unitOfWork.GetWriteRepository<OrderStatusHistoryEntity>()
                .AddAsync(history);

            await _unitOfWork.SaveAsync();

            return new ShipOrderCommandResponse
            {
                OrderId = order.Id,
                CargoCompany = order.CargoCompany!,
                TrackingNumber = order.TrackingNumber!,
                ShippedDate = order.ShippedDate
            };
        }
    }
}
