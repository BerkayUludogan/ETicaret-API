using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Orders.DTOs;
using ETicaret.Domain.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQueryRequest, List<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderDto>> Handle(GetOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork
                .GetReadRepository<OrderEntity>()
                .GetWhere(x => !x.IsDeleted, false)
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
                        .Where(y => !y.IsDeleted)
                        .Select(y => new OrderItemDto
                        {

                            OrderItemId = y.Id,
                            ProductId = y.ProductId,
                            ProductName = y.ProductName,
                            ProductSku = y.ProductSku,
                            UnitPrice = y.UnitPrice,
                            Quantity = y.Quantity,
                            TotalPrice = y.TotalPrice
                        }).ToList()
                }).ToListAsync(cancellationToken);

            return orders;
        }
    }
}
