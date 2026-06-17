using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ETicaret.Application.Features.Baskets.Commands.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandRequest : IRequest<UpdateBasketItemQuantityCommandResponse>
    {
        [JsonIgnore]
        [BindNever]
        public Guid UserId { get; set; }
        [JsonIgnore]
        [BindNever]
        public Guid BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
