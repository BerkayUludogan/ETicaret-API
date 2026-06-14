using ETicaret.Application.Common.Enums;
using ETicaret.Application.Features.Products.Queries.GetProducts;
using ETicaret.Domain.Entities.Catalog;

namespace ETicaret.Application.Features.Products.Extensions
{
    public static class ProductQueryExtensions
    {
        public static IQueryable<ProductEntity> ApplyFilters(
            this IQueryable<ProductEntity> query, GetProductsQueryRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    x.Slug.ToLower().Contains(search) ||
                    x.SKU.ToLower().Contains(search));
            }
            if (request.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == request.CategoryId.Value);
            }
            if (request.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= request.MaxPrice.Value);
            }
            if (request.IsFeatured.HasValue)
            {
                query = query.Where(x => x.IsFeatured == request.IsFeatured.Value);
            }
            return query;
        }
        public static IQueryable<ProductEntity> ApplySorting(this IQueryable<ProductEntity> query, GetProductsQueryRequest request)
        {
            var sortBy = request.SortBy ?? ProductSortBy.Name;
            var sortDirection = request.SortDirection ?? SortDirection.Asc;

            var isDescending = sortDirection == SortDirection.Desc;

            return sortBy switch
            {
                ProductSortBy.Price => isDescending
                ? query.OrderByDescending(x => x.Price)
                : query.OrderBy(x => x.Price),
                ProductSortBy.Name => isDescending
                ? query.OrderByDescending(x => x.Name)
                : query.OrderBy(x => x.Name),
                ProductSortBy.CreatedDate => isDescending
                ? query.OrderByDescending(x => x.CreatedDate)
                : query.OrderBy(x => x.CreatedDate),

                _ => query.OrderBy(x => x.Name)
            };
        }
    }

}
