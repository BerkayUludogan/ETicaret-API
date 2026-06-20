using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Application.Features.Baskets.Rules;
using ETicaret.Domain.Entities.Catalog;

namespace ETicaret.Tests.Features.Baskets.Rules
{
    public class BasketBusinessRulesTests
    {
        private readonly BasketBusinessRules _rules = new(null!);

        [Theory]
        [InlineData(10, 1)]
        [InlineData(10, 10)]
        public void ProductStockMustBeEnough_WhenStockIsEnough_ShouldNotThrow(
        int stockQuantity,
        int requestedQuantity)
        {
            var product = new ProductEntity
            {
                StockQuantity = stockQuantity
            };

            var exception = Record.Exception(() =>
                _rules.ProductStockMustBeEnough(product, requestedQuantity));

            Assert.Null(exception);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(5, 6)]
        public void ProductStockMustBeEnough_WhenStockIsNotEnough_ShouldThrowBusinessRuleException(
        int stockQuantity,
        int requestedQuantity)
        {
            var product = new ProductEntity
            {
                StockQuantity = stockQuantity
            };

            var exception = Assert.Throws<BusinessRuleException>(() =>
                _rules.ProductStockMustBeEnough(product, requestedQuantity));

            Assert.Equal(422, exception.StatusCode);
            Assert.Contains(ErrorMessageResolver.Get(ProductErrors.ProductStockNotEnough), exception.Errors);
        }

    }
}
