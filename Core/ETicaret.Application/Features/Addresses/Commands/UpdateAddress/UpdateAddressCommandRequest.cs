using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ETicaret.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandRequest : IRequest<UpdateAddressCommandResponse>
    {
        [JsonIgnore]
        [BindNever]
        public Guid UserId { get; set; } 
        [JsonIgnore]
        [BindNever]
        public Guid AddressId { get; set; }

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
