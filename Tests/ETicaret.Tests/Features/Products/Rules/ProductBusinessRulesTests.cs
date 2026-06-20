using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Application.Features.Products.Rules;

namespace ETicaret.Tests.Features.Products.Rules;

public class ProductBusinessRulesTests
{
    private readonly ProductBusinessRules _rules = new(null!);

    [Theory]
    [InlineData(100, 50)]
    [InlineData(100, 99.99)]
    public async Task DiscountPriceMustBeLessThanPrice_WhenDiscountPriceIsLower_ShouldNotThrow(
        decimal price,
        decimal discountPrice)
    {
        var exception = await Record.ExceptionAsync(() =>
            _rules.DiscountPriceMustBeLessThanPrice(price, discountPrice));

        Assert.Null(exception);
    }

    [Fact]
    public async Task DiscountPriceMustBeLessThanPrice_WhenDiscountPriceIsNull_ShouldNotThrow()
    {
        var exception = await Record.ExceptionAsync(() =>
            _rules.DiscountPriceMustBeLessThanPrice(100, null));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(100, 100)]
    [InlineData(100, 150)]
    public async Task DiscountPriceMustBeLessThanPrice_WhenDiscountPriceIsEqualOrGreater_ShouldThrowBusinessRuleException(
        decimal price,
        decimal discountPrice)
    {
        var exception = await Assert.ThrowsAsync<BusinessRuleException>(() =>
            _rules.DiscountPriceMustBeLessThanPrice(price, discountPrice));

        Assert.Equal(422, exception.StatusCode);
        Assert.Contains(ErrorMessageResolver.Get(ProductErrors.DiscountPriceMustBeLessThanPrice), exception.Errors);
    }
}
