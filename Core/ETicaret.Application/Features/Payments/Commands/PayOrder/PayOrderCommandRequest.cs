using ETicaret.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ETicaret.Application.Features.Payments.Commands.PayOrder
{
    public class PayOrderCommandRequest : IRequest<PayOrderCommandResponse>
    {
        [JsonIgnore]
        [BindNever]
        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
