using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Domain.Entities.Basket;
using ETicaret.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Baskets.Rules
{
    public class BasketBusinessRules : IBasketBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public BasketBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductEntity> ProductMustExistAndBeActiveAsync(Guid productId)
        {
            var product = await _unitOfWork
                .GetReadRepository<ProductEntity>()
                .GetSingleAsync(x => x.Id == productId && x.IsActive && !x.IsDeleted, false);
            if (product == null)
                throw new BusinessRuleException(ProductErrors.ProductNotFound);

            return product;
        }

        public void ProductStockMustBeEnough(ProductEntity product, int quantity)
        {
            if (product.StockQuantity < quantity)
                throw new BusinessRuleException(ProductErrors.ProductStockNotEnough);
        }
        public async Task<BasketItemEntity> BasketItemMustExistAsync(Guid userId, Guid basketItemId)
        {
            var basketItem = await _unitOfWork
                .GetReadRepository<BasketItemEntity>()
                .GetSingleAsync(x =>
                x.Id == basketItemId &&
                !x.IsDeleted &&
                x.Basket.UserId == userId &&
                !x.Basket.IsDeleted, true);

            if (basketItem == null)
                throw new BusinessRuleException(BasketErrors.BasketItemNotFound);
            
            return basketItem;
        }

        public async Task<BasketEntity> BasketMustExistAsync(Guid userId)
        {
            var basket = await _unitOfWork
                .GetReadRepository<BasketEntity>()
                .GetWhere(x=>x.UserId == userId && !x.IsDeleted,true)
                .Include(x=>x.Items).FirstOrDefaultAsync();

            if (basket is null)
                throw new BusinessRuleException(BasketErrors.BasketNotFound);

            return basket;
        }
    }
}
