using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Addresses.Rules;
using ETicaret.Domain.Entities.Address;
using MediatR;

namespace ETicaret.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommandRequest, CreateAddressCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressBusinessRules _addressBusinessRules;

        public CreateAddressCommandHandler(IUnitOfWork unitOfWork, IAddressBusinessRules addressBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _addressBusinessRules = addressBusinessRules;
        }

        public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommandRequest request, CancellationToken cancellationToken)
        {
            await _addressBusinessRules.ResetDefaultAddressesIfNeededAsync(request.UserId, request.IsDefault);

            var address = new AddressEntity
            {
                UserId = request.UserId,
                Title = request.Title,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Country = request.Country,
                City = request.City,
                District = request.District,
                Neighborhood = request.Neighborhood,
                AddressLine = request.AddressLine,
                PostalCode = request.PostalCode,
                IsDefault = request.IsDefault
            };

            await _unitOfWork.GetWriteRepository<AddressEntity>()
                .AddAsync(address);
            await _unitOfWork.SaveAsync();

            return new CreateAddressCommandResponse { Id = address.Id };
        }
    }
}
