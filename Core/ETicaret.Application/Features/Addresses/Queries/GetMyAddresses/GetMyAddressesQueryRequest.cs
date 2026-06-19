using ETicaret.Application.Features.Addresses.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Addresses.Queries.GetMyAddresses
{
    public class GetMyAddressesQueryRequest : IRequest<List<AddressDto>>
    {
        public Guid UserId { get; set; }
    }
}
