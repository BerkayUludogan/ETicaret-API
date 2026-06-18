using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Orders.Rules;
using ETicaret.Domain.Entities.Basket;
using ETicaret.Domain.Entities.Catalog;
using ETicaret.Domain.Entities.Order;
using MediatR;

namespace ETicaret.Application.Features.Orders.Commands.CreateOrderFromBasket
{
    public class CreateOrderFromBasketCommandHandler : IRequestHandler<CreateOrderFromBasketCommandRequest, CreateOrderFromBasketCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderBusinessRules _orderBusinessRules;

        public CreateOrderFromBasketCommandHandler(IUnitOfWork unitOfWork, IOrderBusinessRules orderBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<CreateOrderFromBasketCommandResponse> Handle(CreateOrderFromBasketCommandRequest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var basket = await _orderBusinessRules
                    .UserBasketMustExistWithItemsAsync(request.UserId);

                _orderBusinessRules.BasketMustNotBeEmpty(basket);
                foreach (var basketItem in basket.Items)
                {
                    _orderBusinessRules.ProductStockMustBeEnough(basketItem.Product.StockQuantity, basketItem.Quantity);
                }

                var order = CreateOrder(request, basket);

                await _unitOfWork
                    .GetWriteRepository<OrderEntity>()
                    .AddAsync(order);

                foreach (var basketItem in basket.Items)
                {
                    basketItem.Product.StockQuantity -= basketItem.Quantity;
                    basketItem.IsDeleted = true;
                }

                _unitOfWork
                    .GetWriteRepository<BasketItemEntity>()
                    .UpdateRange(basket.Items.ToList());

                _unitOfWork
                    .GetWriteRepository<ProductEntity>()
                    .UpdateRange(basket.Items.Select(x => x.Product).ToList());

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new CreateOrderFromBasketCommandResponse
                {
                    OrderId = order.Id,
                    TotalPrice = order.TotalPrice
                };
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private static OrderEntity CreateOrder(
            CreateOrderFromBasketCommandRequest request, BasketEntity basket)
        {
            var orderItems = basket.Items
                .Select(x =>
                {
                    var unitPrice = x.Product.DiscountPrice ?? x.Product.Price;

                    return new OrderItemEntity
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        ProductSku = x.Product.SKU,
                        UnitPrice = unitPrice,
                        Quantity = x.Quantity,
                        TotalPrice = unitPrice * x.Quantity
                    };
                })
                .ToList();

            return new OrderEntity
            {
                UserId = request.UserId,
                ShippingAddress = request.ShippingAddress,
                Items = orderItems,
                TotalPrice = orderItems.Sum(x => x.TotalPrice),
            };
        }
    }
}
