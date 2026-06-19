using ETicaret.Application.Common.Constants.FieldLengths;
using ETicaret.Domain.Entities.Address;
using ETicaret.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.Persistence.Configuration.Address
{
    public class AddressConfiguration : BaseEntityConfiguration<AddressEntity>
    {
        public override void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Addresses");

            builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(AddressFieldLengths.Title);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(AddressFieldLengths.FullName);

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(CommonFieldLengths.PhoneNumber);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(AddressFieldLengths.Country);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(AddressFieldLengths.City);

            builder.Property(x => x.District)
                .IsRequired()
                .HasMaxLength(AddressFieldLengths.District);

            builder.Property(x => x.Neighborhood)
                .IsRequired()
                .HasMaxLength(AddressFieldLengths.Neighborhood);

            builder.Property(x => x.AddressLine)
                .IsRequired()
                .HasMaxLength(AddressFieldLengths.AddressLine);

            builder.Property(x => x.PostalCode)
                .HasMaxLength(AddressFieldLengths.PostalCode);

            builder.Property(x => x.IsDefault)
                .HasDefaultValue(false);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.UserId);

        }
    }
}