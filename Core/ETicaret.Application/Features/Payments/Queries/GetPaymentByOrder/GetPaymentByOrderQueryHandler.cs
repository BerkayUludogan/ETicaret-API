using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Application.Features.Payments.DTOs;
using ETicaret.Domain.Entities.Payment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Payments.Queries.GetPaymentByOrder
{
    public class GetPaymentByOrderQueryHandler : IRequestHandler<GetPaymentByOrderQueryRequest, PaymentDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaymentByOrderQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PaymentDto> Handle(GetPaymentByOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork
               .GetReadRepository<PaymentEntity>()
               .GetWhere(x =>
                   x.OrderId == request.OrderId &&
                   x.UserId == request.UserId &&
                   !x.IsDeleted,
                   false)
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
               }).FirstOrDefaultAsync(cancellationToken);

            if (payment is null)
                throw new BusinessRuleException(PaymentErrors.PaymentNotFound);

            return payment;
        }
    }
}