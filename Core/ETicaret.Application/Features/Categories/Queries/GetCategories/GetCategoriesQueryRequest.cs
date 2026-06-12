using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Common.Constants;
using ETicaret.Application.Features.Categories.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryRequest : BaseCacheableQuery<List<CategoryListDto>>
    {
        protected override string CacheKeyPrefix => CacheKeys.AllCategories.Key;
        public override double? ExpirationMinutes => CacheKeys.AllCategories.Time;
    }
}
