using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Application.Features.Orders.DTOs;
using ETicaret.Domain.Entities.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork
                .GetReadRepository<OrderEntity>()
                .GetWhere(x => x.Id == request.OrderId && !x.IsDeleted, false);

            if (!request.IsAdmin)
                query = query.Where(x => x.UserId == request.UserId);

            var order = await query
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

                }).FirstOrDefaultAsync();

            if (order is null)
                throw new BusinessRuleException(OrderErrors.OrderNotFound);

            return order;
        }
    }
}
