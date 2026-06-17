using ETicaret.Domain.Entities.Basket;
using ETicaret.Domain.Entities.Catalog;

namespace ETicaret.Application.Features.Baskets.Rules
{
    public interface IBasketBusinessRules
    {
        Task<ProductEntity> ProductMustExistAndBeActiveAsync(Guid productId);
        void ProductStockMustBeEnough(ProductEntity product, int quantity);
        Task<BasketItemEntity> BasketItemMustExistAsync(Guid userId, Guid basketItemId);
        Task<BasketEntity> BasketMustExistAsync(Guid userId);

    }
}
