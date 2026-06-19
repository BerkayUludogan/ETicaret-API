using ETicaret.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ETicaret.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandRequest : IRequest<UpdateOrderStatusCommandResponse>
    {
        [JsonIgnore]
        [BindNever]
        public Guid OrderId { get; set; }
        [JsonIgnore]
        [BindNever]
        public Guid? ChangedByUserId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
