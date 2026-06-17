using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Baskets.Rules;
using ETicaret.Domain.Entities.Basket;
using MediatR;

namespace ETicaret.Application.Features.Baskets.Commands.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandHandler : IRequestHandler<UpdateBasketItemQuantityCommandRequest, UpdateBasketItemQuantityCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketBusinessRules _basketBusinessRules;

        public UpdateBasketItemQuantityCommandHandler(IUnitOfWork unitOfWork, IBasketBusinessRules basketBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<UpdateBasketItemQuantityCommandResponse> Handle(UpdateBasketItemQuantityCommandRequest request, CancellationToken cancellationToken)
        {
            var basketItem = await _basketBusinessRules
               .BasketItemMustExistAsync(request.UserId, request.BasketItemId);

            var product = await _basketBusinessRules
                .ProductMustExistAndBeActiveAsync(basketItem.ProductId);

            _basketBusinessRules.ProductStockMustBeEnough(product, request.Quantity);

            basketItem.Quantity = request.Quantity;

            _unitOfWork
                .GetWriteRepository<BasketItemEntity>()
                .Update(basketItem);

            await _unitOfWork.SaveAsync();

            return new UpdateBasketItemQuantityCommandResponse
            {
                BasketItemId = basketItem.Id,
                Quantity = basketItem.Quantity
            };
        }
    }
}
