using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Addresses.Rules;
using ETicaret.Domain.Entities.Address;
using MediatR;

namespace ETicaret.Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommandRequest, DeleteAddressCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressBusinessRules _addressBusinessRules;

        public DeleteAddressCommandHandler(IUnitOfWork unitOfWork, IAddressBusinessRules addressBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _addressBusinessRules = addressBusinessRules;
        }

        public async Task<DeleteAddressCommandResponse> Handle(DeleteAddressCommandRequest request, CancellationToken cancellationToken)
        {
            var address = await _addressBusinessRules.AddressMustExistForUser(request.UserId, request.AddressId);

            address.IsDeleted = true;
            address.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.GetWriteRepository<AddressEntity>()
                .Update(address);

            await _unitOfWork.SaveAsync();

            return new DeleteAddressCommandResponse
            {
                Id = address.Id
            };
        }
    }
}
