using ETicaret.Application.Features.Payments.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Payments.Queries.GetPaymentByOrder
{
    public class GetPaymentByOrderQueryRequest : IRequest<PaymentDto>
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
