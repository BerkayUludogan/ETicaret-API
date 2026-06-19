using ETicaret.Application.Features.Payments.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Payments.Queries.GetMyPayments
{
    public class GetMyPaymentsQueryRequest : IRequest<List<PaymentDto>>
    {
        public Guid UserId { get; set; }
    }
}
