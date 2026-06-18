using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Domain.Entities.Basket;
using ETicaret.Domain.Entities.Order;
using ETicaret.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Orders.Rules
{
    public class OrderBusinessRules : IOrderBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void BasketMustNotBeEmpty(BasketEntity basket)
        {
            if (basket.Items.Count == 0)
                throw new BusinessRuleException(OrderErrors.BasketIsEmpty);
        }

        public void ProductStockMustBeEnough(int stockQuantity, int requestedQuantity)
        {
            if (stockQuantity < requestedQuantity)
                throw new BusinessRuleException(OrderErrors.ProductStockNotEnough);
        }

        public async Task<BasketEntity> UserBasketMustExistWithItemsAsync(Guid userId)
        {

            var basket = await _unitOfWork
                .GetReadRepository<BasketEntity>()
                .GetWhere(x => x.UserId == userId && !x.IsDeleted, true)
                .Include(x => x.Items.Where(i => !i.IsDeleted))
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync();

            if (basket is null)
                throw new BusinessRuleException(OrderErrors.BasketNotFound);

            return basket;
        }
        public async Task<OrderEntity> OrderMustExist(Guid orderId)
        {
            var order = await _unitOfWork
                .GetReadRepository<OrderEntity>()
                .GetWhere(x => x.Id == orderId && !x.IsDeleted, true)
                .FirstOrDefaultAsync();

            if (order is null)
                throw new BusinessRuleException(OrderErrors.OrderNotFound);

            return order;
        }

        public void CompletedOrderStatusCannotBeChanged(OrderEntity order)
        {
            if (order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Cancelled)
                throw new BusinessRuleException(OrderErrors.CompletedOrderStatusCannotBeChanged);
        }
    }
}