using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ETicaret.Application.Features.Orders.Commands.CreateOrderFromBasket
{
    public class CreateOrderFromBasketCommandRequest : IRequest<CreateOrderFromBasketCommandResponse>
    {
        [JsonIgnore]
        [BindNever]
        public Guid UserId { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
    }
}
