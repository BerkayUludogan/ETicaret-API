using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Payments.DTOs;
using ETicaret.Domain.Entities.Payment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Payments.Queries.GetMyPayments
{
    public class GetMyPaymentsQueryHandler : IRequestHandler<GetMyPaymentsQueryRequest, List<PaymentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMyPaymentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<PaymentDto>> Handle(GetMyPaymentsQueryRequest request, CancellationToken cancellationToken)
        {
            var payments = await _unitOfWork
                .GetReadRepository<PaymentEntity>()
                .GetWhere(x => x.UserId == request.UserId && !x.IsDeleted, false)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new PaymentDto
                {
                    PaymentId = x.Id,
                    OrderId = x.OrderId,
                    UserId = x.UserId,
                    Amount = x.Amount,
                    PaymentMethod = x.PaymentMethod.ToString(),
                    Status = x.Status.ToString(),
                    TransactionId = x.TransactionId,
                    PaidDate = x.PaidDate,
                    FailedReason = x.FailedReason,
                    CreatedDate = x.CreatedDate
                })
                .ToListAsync(cancellationToken);

            return payments;
        }
    }
}
