namespace ETicaret.Application.Features.Addresses.DTOs
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public string? PostalCode { get; set; }

        public bool IsDefault { get; set; }
    }
}