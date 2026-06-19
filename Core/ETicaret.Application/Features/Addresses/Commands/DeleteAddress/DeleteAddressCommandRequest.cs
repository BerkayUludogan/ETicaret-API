using MediatR;

namespace ETicaret.Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommandRequest : IRequest<DeleteAddressCommandResponse>
    {
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
    }
}
