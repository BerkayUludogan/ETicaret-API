using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Baskets.Rules;
using ETicaret.Domain.Entities.Basket;
using MediatR;

namespace ETicaret.Application.Features.Baskets.Commands.RemoveBasketItem
{
    public class RemoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketBusinessRules _basketBusinessRules;

        public RemoveBasketItemCommandHandler(IUnitOfWork unitOfWork, IBasketBusinessRules basketBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<RemoveBasketItemCommandResponse> Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            var basketItem = await _basketBusinessRules
               .BasketItemMustExistAsync(request.UserId, request.BasketItemId);

            basketItem.IsDeleted = true;

            _unitOfWork
                .GetWriteRepository<BasketItemEntity>()
                .Update(basketItem);

            await _unitOfWork.SaveAsync();

            return new RemoveBasketItemCommandResponse
            {
                BasketItemId = basketItem.Id
            };
        }
    }
}
