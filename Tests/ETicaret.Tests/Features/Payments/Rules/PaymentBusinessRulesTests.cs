using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Application.Features.Payments.Rules;
using ETicaret.Domain.Entities.Order;
using ETicaret.Domain.Enums;

namespace ETicaret.Tests.Features.Payments.Rules
{
    public class PaymentBusinessRulesTests
    {
        private readonly PaymentBusinessRules _rules = new(null!);

        [Fact]
        public void OrderMustBePending_WhenOrderStatusIsPending_ShouldNotThrow()
        {
            var order = new OrderEntity
            {
                Status = OrderStatus.Pending
            };

            var exception = Record.Exception(() => _rules.OrderMustBePending(order));

            Assert.Null(exception);
        }

        [Theory]
        [InlineData(OrderStatus.Paid)]
        [InlineData(OrderStatus.Preparing)]
        [InlineData(OrderStatus.Shipped)]
        [InlineData(OrderStatus.Delivered)]
        [InlineData(OrderStatus.Cancelled)]
        public void OrderMustBePending_WhenOrderStatusIsNotPending_ShouldThrowBusinessRuleException(OrderStatus status)
        {
            var order = new OrderEntity
            {
                Status = status
            };

            var exception = Assert.Throws<BusinessRuleException>(() =>
           _rules.OrderMustBePending(order));

            Assert.Equal(422, exception.StatusCode);
            Assert.Contains(ErrorMessageResolver.Get(PaymentErrors.OrderIsNotPending), exception.Errors);
        }

    }
}
