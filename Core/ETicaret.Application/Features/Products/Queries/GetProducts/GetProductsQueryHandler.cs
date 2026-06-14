using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Products.DTOs;
using ETicaret.Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, List<ProductListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductListDto>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork
                .GetReadRepository<ProductEntity>()
                .GetWhere(x => !x.IsDeleted && x.IsActive, false)
                .OrderBy(x => x.Name)
                .Select(x => new ProductListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Description = x.Description ?? string.Empty,
                    Price = x.Price,
                    DiscountPrice = x.DiscountPrice,
                    StockQuantity = x.StockQuantity,
                    SKU = x.SKU,
                    IsActive = x.IsActive,
                    IsFeatured = x.IsFeatured,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name
                }).ToListAsync(cancellationToken);
        }
    }
}
