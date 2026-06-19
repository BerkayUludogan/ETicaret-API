using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Orders.Rules;
using ETicaret.Domain.Entities.Order;
using MediatR;

namespace ETicaret.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommandRequest, UpdateOrderStatusCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderBusinessRules _rules;

        public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork, IOrderBusinessRules rules)
        {
            _unitOfWork = unitOfWork;
            _rules = rules;
        }

        public async Task<UpdateOrderStatusCommandResponse> Handle(UpdateOrderStatusCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _rules.OrderMustExist(request.OrderId);
            var oldStatus = order.Status;
            _rules.CompletedOrderStatusCannotBeChanged(order);

            order.Status = request.Status;
            order.ModifiedDate = DateTime.UtcNow;

            var history = new OrderStatusHistoryEntity
            {
                OrderId = order.Id,
                OldStatus = oldStatus,
                NewStatus = order.Status,
                ChangedByUserId = request.ChangedByUserId
            };

            _unitOfWork.GetWriteRepository<OrderEntity>().Update(order);
            await _unitOfWork.GetWriteRepository<OrderStatusHistoryEntity>().AddAsync(history);

            await _unitOfWork.SaveAsync();

            return new UpdateOrderStatusCommandResponse
            {
                OrderId = order.Id,
                Status = order.Status,
            };
        }
    }
}