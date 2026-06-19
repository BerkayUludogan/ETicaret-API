using ETicaret.Domain.Entities.Address;

namespace ETicaret.Application.Features.Addresses.Rules
{
    public interface IAddressBusinessRules
    {
        Task ResetDefaultAddressesIfNeededAsync(Guid userId, bool isDefault);
        Task<AddressEntity> AddressMustExistForUser(Guid userId, Guid addressId);
    }
}
