using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Domain.Entities.Address;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Addresses.Rules
{
    public class AddressBusinessRules : IAddressBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddressBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddressEntity> AddressMustExistForUser(Guid userId, Guid addressId)
        {
            var address = await _unitOfWork
                  .GetReadRepository<AddressEntity>()
                  .GetWhere(x =>
                  x.Id == addressId &&
                  x.UserId == userId && !x.IsDeleted, true).FirstOrDefaultAsync();

            if (address is null)
                throw new BusinessRuleException(AddressErrors.AddressNotFound);

            return address;
        }

        public async Task ResetDefaultAddressesIfNeededAsync(Guid userId, bool isDefault)
        {
            if (!isDefault)
                return;

            var defaultAddresses = await _unitOfWork
                .GetReadRepository<AddressEntity>()
                .GetWhere(x => x.UserId == userId && x.IsDefault && !x.IsDeleted, true).ToListAsync();

            if (defaultAddresses.Count == 0) return;

            foreach (var address in defaultAddresses)
            {
                address.IsDefault = false;
                address.ModifiedDate = DateTime.UtcNow;
            }

            _unitOfWork.GetWriteRepository<AddressEntity>()
                .UpdateRange(defaultAddresses);
        }
    }
}