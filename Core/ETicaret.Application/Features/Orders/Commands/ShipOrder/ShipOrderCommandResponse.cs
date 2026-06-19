namespace ETicaret.Application.Features.Orders.Commands.ShipOrder
{
    public class ShipOrderCommandResponse
    {
        public Guid OrderId { get; set; }
        public string CargoCompany { get; set; } = string.Empty;
        public string TrackingNumber { get; set; } = string.Empty;
        public DateTime? ShippedDate { get; set; }
    }
}
