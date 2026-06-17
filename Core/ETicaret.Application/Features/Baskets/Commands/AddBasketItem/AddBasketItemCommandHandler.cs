using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Baskets.Rules;
using ETicaret.Domain.Entities.Basket;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Baskets.Commands.AddBasketItem
{
    public class AddBasketItemCommandHandler : IRequestHandler<AddBasketItemCommandRequest, AddBasketItemCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketBusinessRules _basketBusinessRules;

        public AddBasketItemCommandHandler(IUnitOfWork unitOfWork, IBasketBusinessRules basketBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<AddBasketItemCommandResponse> Handle(AddBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _basketBusinessRules.ProductMustExistAndBeActiveAsync(request.ProductId);

            _basketBusinessRules.ProductStockMustBeEnough(product, request.Quantity);
            
            var basket = await GetOrCreateBasket(request.UserId, cancellationToken);

            var basketItem = await AddOrUpdateBasketItem(
                basket,
                request.ProductId,
                request.Quantity);

            await _unitOfWork.SaveAsync();
          
            return new AddBasketItemCommandResponse
            {
                BasketId = basket.Id,
                ProductId = request.ProductId,
                Quantity = basketItem.Quantity
            };
        }
        private async Task<BasketEntity> GetOrCreateBasket(
        Guid userId, CancellationToken cancellationToken)
        {
            var basket = await _unitOfWork
                .GetReadRepository<BasketEntity>()
                .GetWhere(x => x.UserId == userId && !x.IsDeleted, true)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(cancellationToken);

            if (basket is not null)
                return basket;

            basket = new BasketEntity
            {
                UserId = userId,
                Items = []
            };
            await _unitOfWork.GetWriteRepository<BasketEntity>().AddAsync(basket);
            return basket;
        }
        private async Task<BasketItemEntity> AddOrUpdateBasketItem(
            BasketEntity basket, Guid productId, int quantity)
        {
            var basketItem = basket.Items
                .FirstOrDefault(x => x.ProductId == productId && !x.IsDeleted);

            if (basketItem is not null)
            {
                basketItem.Quantity += quantity;

                _unitOfWork.GetWriteRepository<BasketItemEntity>().Update(basketItem);
                return basketItem;
            }
            basketItem = new BasketItemEntity
            {
                Basket = basket,
                ProductId = productId,
                Quantity = quantity
            };
            await _unitOfWork.GetWriteRepository<BasketItemEntity>().AddAsync(basketItem);
            return basketItem;
        }
    }
}
