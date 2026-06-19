using ETicaret.Application.Common.Constants.FieldLengths;
using FluentValidation;

namespace ETicaret.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommandRequest>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(AddressFieldLengths.Title);

            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(AddressFieldLengths.FullName);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MaximumLength(CommonFieldLengths.PhoneNumber);

            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(AddressFieldLengths.Country);

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(AddressFieldLengths.City);

            RuleFor(x => x.District)
                .NotEmpty()
                .MaximumLength(AddressFieldLengths.District);

            RuleFor(x => x.Neighborhood)
                .NotEmpty()
                .MaximumLength(AddressFieldLengths.Neighborhood);

            RuleFor(x => x.AddressLine)
                .NotEmpty()
                .MaximumLength(AddressFieldLengths.AddressLine);

            RuleFor(x => x.PostalCode)
                .MaximumLength(AddressFieldLengths.PostalCode);
        }
    }
}
