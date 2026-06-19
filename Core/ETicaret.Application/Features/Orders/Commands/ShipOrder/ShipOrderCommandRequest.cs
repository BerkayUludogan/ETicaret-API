using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ETicaret.Application.Features.Orders.Commands.ShipOrder
{
    public class ShipOrderCommandRequest : IRequest<ShipOrderCommandResponse>
    {
        [JsonIgnore]
        [BindNever]
        public Guid OrderId { get; set; }

        [JsonIgnore]
        [BindNever]
        public Guid ChangedByUserId { get; set; }

        public string CargoCompany { get; set; } = string.Empty;
        public string TrackingNumber { get; set; } = string.Empty;
    }
}
