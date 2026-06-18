using ETicaret.Domain.Entities.Basket;

namespace ETicaret.Application.Features.Orders.Rules
{
    public interface IOrderBusinessRules
    {
        Task<BasketEntity> UserBasketMustExistWithItemsAsync(Guid userId);
        void BasketMustNotBeEmpty(BasketEntity basket);
        void ProductStockMustBeEnough(int stockQuantity, int requestedQuantity);
    }
}
