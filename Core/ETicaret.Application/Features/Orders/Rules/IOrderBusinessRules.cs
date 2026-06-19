using ETicaret.Domain.Entities.Basket;
using ETicaret.Domain.Entities.Order;
using ETicaret.Domain.Enums;

namespace ETicaret.Application.Features.Orders.Rules
{
    public interface IOrderBusinessRules
    {
        Task<BasketEntity> UserBasketMustExistWithItemsAsync(Guid userId);
        Task<OrderEntity> OrderMustExist(Guid orderId);
        void BasketMustNotBeEmpty(BasketEntity basket);
        void ProductStockMustBeEnough(int stockQuantity, int requestedQuantity);
        void CompletedOrderStatusCannotBeChanged(OrderEntity order);
        Task<OrderEntity> OrderMustExistWithItems(Guid orderId);
        void OrderStatusTransitionMustBeValid(OrderStatus currentStatus, OrderStatus newStatus);
        void OrderMustBeShippable(OrderEntity order);
    }
}
