using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Addresses.Rules;
using ETicaret.Domain.Entities.Address;
using MediatR;

namespace ETicaret.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommandRequest, UpdateAddressCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressBusinessRules _addressBusinessRules;

        public UpdateAddressCommandHandler(IUnitOfWork unitOfWork, IAddressBusinessRules addressBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _addressBusinessRules = addressBusinessRules;
        }

        public async Task<UpdateAddressCommandResponse> Handle(UpdateAddressCommandRequest request, CancellationToken cancellationToken)
        {
            var address = await _addressBusinessRules.AddressMustExistForUser(request.UserId, request.AddressId);
            
            await _addressBusinessRules.ResetDefaultAddressesIfNeededAsync(request.UserId, request.IsDefault);

            address.Title = request.Title;
            address.FullName = request.FullName;
            address.PhoneNumber = request.PhoneNumber;
            address.Country = request.Country;
            address.City = request.City;
            address.District = request.District;
            address.Neighborhood = request.Neighborhood;
            address.AddressLine = request.AddressLine;
            address.PostalCode = request.PostalCode;
            address.IsDefault = request.IsDefault;
            address.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.GetWriteRepository<AddressEntity>()
                .Update(address);

            await _unitOfWork.SaveAsync();

            return new UpdateAddressCommandResponse
            {
                Id = address.Id
            };
        }
    }
}