using ETicaret.Domain.Entities.Address;

namespace ETicaret.Application.Helper
{
    public static class AddressHelper
    {
        public static string BuildShippingAddress(AddressEntity address)
        {
            return $"{address.FullName}, {address.PhoneNumber}, {address.Country}, {address.City}, {address.District}, {address.Neighborhood}, {address.AddressLine}"
                 + (string.IsNullOrWhiteSpace(address.PostalCode)
                    ? string.Empty
                    : $", {address.PostalCode}");
        }
    }
}
