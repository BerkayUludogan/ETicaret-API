using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Domain.Entities.Address
{
    public class AddressEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUserEntity User { get; set; } = default!;

        public string Title { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string Country { get; set; } = "Türkiye";
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public string? PostalCode { get; set; }

        public bool IsDefault { get; set; }
    }
}
