using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Addresses.DTOs;
using ETicaret.Domain.Entities.Address;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Addresses.Queries.GetMyAddresses
{
    public class GetMyAddressesQueryHandler : IRequestHandler<GetMyAddressesQueryRequest, List<AddressDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMyAddressesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AddressDto>> Handle(GetMyAddressesQueryRequest request, CancellationToken cancellationToken)
        {
            var addresses = await _unitOfWork
               .GetReadRepository<AddressEntity>()
               .GetWhere(x => x.UserId == request.UserId && !x.IsDeleted, false)
               .OrderByDescending(x => x.IsDefault)
               .ThenByDescending(x => x.CreatedDate)
               .Select(x => new AddressDto
               {
                   Id = x.Id,
                   Title = x.Title,
                   FullName = x.FullName,
                   PhoneNumber = x.PhoneNumber,
                   Country = x.Country,
                   City = x.City,
                   District = x.District,
                   Neighborhood = x.Neighborhood,
                   AddressLine = x.AddressLine,
                   PostalCode = x.PostalCode,
                   IsDefault = x.IsDefault
               })
               .ToListAsync(cancellationToken);

            return addresses;
        }
    }
}
