using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Application.Features.Orders.Rules;
using ETicaret.Domain.Entities.Order;
using ETicaret.Domain.Enums;

namespace ETicaret.Tests.Features.Orders.Rules;

public class OrderBusinessRulesTests
{
    private readonly OrderBusinessRules _rules = new(null!);

    [Theory]
    [InlineData(OrderStatus.Pending, OrderStatus.Paid)]
    [InlineData(OrderStatus.Paid, OrderStatus.Preparing)]
    [InlineData(OrderStatus.Preparing, OrderStatus.Shipped)]
    [InlineData(OrderStatus.Shipped, OrderStatus.Delivered)]
    public void OrderStatusTransitionMustBeValid_WhenTransitionIsValid_ShouldNotThrow(
        OrderStatus currentStatus,
        OrderStatus newStatus)
    {
        var exception = Record.Exception(() =>
            _rules.OrderStatusTransitionMustBeValid(currentStatus, newStatus));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(OrderStatus.Pending, OrderStatus.Shipped)]
    [InlineData(OrderStatus.Paid, OrderStatus.Delivered)]
    [InlineData(OrderStatus.Delivered, OrderStatus.Cancelled)]
    [InlineData(OrderStatus.Cancelled, OrderStatus.Paid)]
    public void OrderStatusTransitionMustBeValid_WhenTransitionIsInvalid_ShouldThrowBusinessRuleException(
        OrderStatus currentStatus,
        OrderStatus newStatus)
    {
        var exception = Assert.Throws<BusinessRuleException>(() =>
            _rules.OrderStatusTransitionMustBeValid(currentStatus, newStatus));

        Assert.Equal(422, exception.StatusCode);
        Assert.Contains(ErrorMessageResolver.Get(OrderErrors.InvalidOrderStatusTransition), exception.Errors);
    }

    [Theory]
    [InlineData(OrderStatus.Paid)]
    [InlineData(OrderStatus.Preparing)]
    public void OrderMustBeShippable_WhenOrderStatusIsPaidOrPreparing_ShouldNotThrow(OrderStatus status)
    {
        var order = new OrderEntity { Status = status };

        var exception = Record.Exception(() => _rules.OrderMustBeShippable(order));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(OrderStatus.Pending)]
    [InlineData(OrderStatus.Shipped)]
    [InlineData(OrderStatus.Delivered)]
    [InlineData(OrderStatus.Cancelled)]
    public void OrderMustBeShippable_WhenOrderStatusIsNotAllowed_ShouldThrowBusinessRuleException(OrderStatus status)
    {
        var order = new OrderEntity { Status = status };

        var exception = Assert.Throws<BusinessRuleException>(() => _rules.OrderMustBeShippable(order));

        Assert.Equal(422, exception.StatusCode);
        Assert.Contains(ErrorMessageResolver.Get(OrderErrors.OrderCannotBeShipped), exception.Errors);
    }
}
