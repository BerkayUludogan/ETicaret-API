using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Baskets.Rules;
using ETicaret.Domain.Entities.Basket;
using MediatR;

namespace ETicaret.Application.Features.Baskets.Commands.ClearBasket
{
    public class ClearBasketCommandHandler : IRequestHandler<ClearBasketCommandRequest, ClearBasketCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketBusinessRules _basketBusinessRules;

        public ClearBasketCommandHandler(IUnitOfWork unitOfWork, IBasketBusinessRules basketBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<ClearBasketCommandResponse> Handle(ClearBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var basket = await _basketBusinessRules
                .BasketMustExistAsync(request.UserId);

            var activeItems = basket.Items
                .Where(x => !x.IsDeleted)
                .ToList();

            foreach (var activeItem in activeItems) 
                activeItem.IsDeleted = true;

            _unitOfWork.GetWriteRepository<BasketItemEntity>()
                .UpdateRange(activeItems);
            await _unitOfWork.SaveAsync();

            return new ClearBasketCommandResponse
            {
                RemovedItemCount = activeItems.Count,
            };
        }
    }
}
