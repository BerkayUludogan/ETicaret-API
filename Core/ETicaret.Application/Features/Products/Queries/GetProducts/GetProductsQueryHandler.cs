using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Products.DTOs;
using ETicaret.Application.Features.Products.Extensions;
using ETicaret.Domain.Entities.Catalog;
using ETicaret.Models.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, PagedResponse<ProductListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<ProductListDto>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork
                .GetReadRepository<ProductEntity>()
                .GetWhere(x => !x.IsDeleted && x.IsActive, false);

            query = query.ApplyFilters(request);
            query = query.ApplySorting(request);

            var pageNumber = request.PageNumber ?? Page.DefaultPageNumber;
            var pageSize = request.PageSize ?? Page.DefaultPageSize;

            var totalCount = await query.CountAsync(cancellationToken);

            var pageInfo = new Page(pageNumber, pageSize, totalCount);

            var products = await query
                .Skip(pageInfo.Skip)
                .Take(pageInfo.PageSize)
                .Select(x => new ProductListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Description = x.Description,
                    Price = x.Price,
                    DiscountPrice = x.DiscountPrice,
                    StockQuantity = x.StockQuantity,
                    SKU = x.SKU,
                    IsActive = x.IsActive,
                    IsFeatured = x.IsFeatured,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name
                }).ToListAsync(cancellationToken);

            return new PagedResponse<ProductListDto>(products, pageInfo);
        }
    }
}
