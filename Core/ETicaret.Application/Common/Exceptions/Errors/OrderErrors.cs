namespace ETicaret.Application.Common.Exceptions.Errors
{
    public static class OrderErrors
    {
        public const string BasketIsEmpty = "Order.BasketIsEmpty";
        public const string BasketNotFound = "Order.BasketNotFound";
        public const string ProductStockNotEnough = "Order.ProductStockNotEnough";
        public const string OrderNotFound = "Order.OrderNotFound";
        public const string CompletedOrderStatusCannotBeChanged = "Order.CompletedOrderStatusCannotBeChanged";
        public const string InvalidOrderStatusTransition = "Order.InvalidOrderStatusTransition";
        public const string OrderCannotBeShipped = "Order.OrderCannotBeShipped";
    }
}
