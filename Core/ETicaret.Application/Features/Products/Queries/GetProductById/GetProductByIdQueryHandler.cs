using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Application.Features.Products.DTOs;
using ETicaret.Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, ProductListDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductListDto> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork
                .GetReadRepository<ProductEntity>()
                .GetWhere(x => x.Id == request.Id && !x.IsDeleted, false)
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
                }).FirstOrDefaultAsync(cancellationToken);

            if (product is null)
                throw new BusinessRuleException(ProductErrors.ProductNotFound);

            return product;
        }
    }
}
