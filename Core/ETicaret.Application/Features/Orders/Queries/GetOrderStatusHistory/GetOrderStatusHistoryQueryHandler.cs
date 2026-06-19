using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Orders.DTOs;
using ETicaret.Application.Features.Orders.Rules;
using ETicaret.Domain.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Orders.Queries.GetOrderStatusHistory
{
    public class GetOrderStatusHistoryQueryHandler : IRequestHandler<GetOrderStatusHistoryQueryRequest, List<OrderStatusHistoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderBusinessRules _orderBusinessRules;

        public GetOrderStatusHistoryQueryHandler(IUnitOfWork unitOfWork, IOrderBusinessRules orderBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<List<OrderStatusHistoryDto>> Handle(GetOrderStatusHistoryQueryRequest request, CancellationToken cancellationToken)
        {
            await _orderBusinessRules.OrderMustExist(request.OrderId);

            var history = await _unitOfWork
                .GetReadRepository<OrderStatusHistoryEntity>()
                .GetWhere(x => x.OrderId == request.OrderId && !x.IsDeleted, false)
                .OrderBy(x => x.CreatedDate)
                .Select(x => new OrderStatusHistoryDto
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    OldStatus = x.OldStatus.ToString(),
                    NewStatus = x.NewStatus.ToString(),
                    ChangedByUserId = x.ChangedByUserId,
                    ChangedByUserName = x.ChangedByUser != null ? x.ChangedByUser.UserName : null,
                    CreatedDate = x.CreatedDate
                }).ToListAsync(cancellationToken);
            return history;
        }
    }
}
