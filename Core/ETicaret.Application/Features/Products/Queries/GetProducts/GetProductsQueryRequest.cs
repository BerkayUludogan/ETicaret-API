using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Common.Constants;
using ETicaret.Application.Common.Enums;
using ETicaret.Application.Features.Products.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ETicaret.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryRequest : CacheablePagedQuery<ProductListDto>
    {
        public string? Search { get; set; }
        public Guid? CategoryId { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public bool? IsFeatured { get; set; }

        public ProductSortBy? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }

        protected override string CacheKeyPrefix => CacheKeys.AllProducts.Key;

        [JsonIgnore]
        [BindNever]
        public override string CacheKey =>
            $"{CacheKeyPrefix}" +
            $"_Page:{PageNumber}" +
            $"_Size:{PageSize}" +
            $"_Search:{Search}" +
            $"_Category:{CategoryId}" +
            $"_Min:{MinPrice}" +
            $"_Max:{MaxPrice}" +
            $"_Featured:{IsFeatured}" +
            $"_SortBy:{SortBy}" +
            $"_Direction:{SortDirection}";

        [JsonIgnore]
        [BindNever]
        public override double? ExpirationMinutes => CacheKeys.AllProducts.Time;
    }
}
