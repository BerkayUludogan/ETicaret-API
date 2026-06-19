using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Orders.DTOs;
using ETicaret.Domain.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Orders.Queries.GetMyOrders
{
    public class GetMyOrdersQueryHandler : IRequestHandler<GetMyOrdersQueryRequest, List<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMyOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<OrderDto>> Handle(GetMyOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork
                .GetReadRepository<OrderEntity>()
                .GetWhere(x => x.UserId == request.UserId && !x.IsDeleted, false)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new OrderDto
                {
                    OrderId = x.Id,
                    UserId = x.UserId,
                    Status = x.Status.ToString(),
                    TotalPrice = x.TotalPrice,
                    ShippingAddress = x.ShippingAddress,
                    CreatedDate = x.CreatedDate,
                    Items = x.Items
                        .Where(i => !i.IsDeleted)
                        .Select(i => new OrderItemDto
                        {
                            OrderItemId = i.Id,
                            ProductId = i.ProductId,
                            ProductName = i.ProductName,
                            ProductSku = i.ProductSku,
                            UnitPrice = i.UnitPrice,
                            Quantity = i.Quantity,
                            TotalPrice = i.TotalPrice
                        })
                        .ToList()
                })
                .ToListAsync(cancellationToken);

            return orders;
        }
    }
}
