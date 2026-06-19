using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ETicaret.Application.Features.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandRequest : IRequest<CancelOrderCommandResponse>
    {
        [JsonIgnore]
        [BindNever]
        public Guid OrderId { get; set; }
        [JsonIgnore]
        [BindNever]
        public Guid? ChangedByUserId { get; set; }
    }
}
