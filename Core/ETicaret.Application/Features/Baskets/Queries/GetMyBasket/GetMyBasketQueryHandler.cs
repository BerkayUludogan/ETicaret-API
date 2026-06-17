using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Baskets.DTOs;
using ETicaret.Domain.Entities.Basket;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Baskets.Queries.GetMyBasket
{
    public class GetMyBasketQueryHandler : IRequestHandler<GetMyBasketQueryRequest, BasketDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMyBasketQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BasketDto> Handle(GetMyBasketQueryRequest request, CancellationToken cancellationToken)
        {
            var basket = await _unitOfWork
                .GetReadRepository<BasketEntity>()
                .GetWhere(x => x.UserId == request.UserId && !x.IsDeleted, false)
                .Select(x => new BasketDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Items = x.Items
                    .Where(y => !y.IsDeleted)
                    .Select(y => new BasketItemDto
                    {
                        Id = y.Id,
                        ProductId = y.ProductId,
                        ProductName = y.Product.Name,
                        ProductSlug = y.Product.Slug,
                        UnitPrice = y.Product.DiscountPrice ?? y.Product.Price,
                        Quantity = y.Quantity,
                        TotalPrice = (y.Product.DiscountPrice ?? y.Product.Price) * y.Quantity
                    })
                    .ToList(),
                    TotalPrice = x.Items
                .Where(y => !y.IsDeleted)
                .Sum(y => (y.Product.DiscountPrice ?? y.Product.Price) * y.Quantity)

                }).FirstOrDefaultAsync(cancellationToken);
            return basket ?? new BasketDto
            {
                UserId = request.UserId,
                Items = [],
                TotalPrice = 0,
            };
        }
    }
}
