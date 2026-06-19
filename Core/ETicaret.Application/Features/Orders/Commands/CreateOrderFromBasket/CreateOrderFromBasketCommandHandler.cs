using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Addresses.Rules;
using ETicaret.Application.Features.Orders.Rules;
using ETicaret.Domain.Entities.Address;
using ETicaret.Domain.Entities.Basket;
using ETicaret.Domain.Entities.Catalog;
using ETicaret.Domain.Entities.Order;
using ETicaret.Application.Helper;
using MediatR;

namespace ETicaret.Application.Features.Orders.Commands.CreateOrderFromBasket
{
    public class CreateOrderFromBasketCommandHandler : IRequestHandler<CreateOrderFromBasketCommandRequest, CreateOrderFromBasketCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderBusinessRules _orderBusinessRules;
        private readonly IAddressBusinessRules _addressBusinessRules;

        public CreateOrderFromBasketCommandHandler(IUnitOfWork unitOfWork, IOrderBusinessRules orderBusinessRules, IAddressBusinessRules addressBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _orderBusinessRules = orderBusinessRules;
            _addressBusinessRules = addressBusinessRules;
        }

        public async Task<CreateOrderFromBasketCommandResponse> Handle(CreateOrderFromBasketCommandRequest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var address = await _addressBusinessRules.AddressMustExistForUser(request.UserId, request.AddressId);

                var basket = await _orderBusinessRules
                    .UserBasketMustExistWithItemsAsync(request.UserId);

                _orderBusinessRules.BasketMustNotBeEmpty(basket);
                foreach (var basketItem in basket.Items)
                {
                    _orderBusinessRules.ProductStockMustBeEnough(basketItem.Product.StockQuantity, basketItem.Quantity);
                }

                var order = CreateOrder(request, basket, address);

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
            CreateOrderFromBasketCommandRequest request, BasketEntity basket, AddressEntity address)
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
                ShippingAddress = AddressHelper.BuildShippingAddress(address),
                Items = orderItems,
                TotalPrice = orderItems.Sum(x => x.TotalPrice),
            };
        }
    }
}
