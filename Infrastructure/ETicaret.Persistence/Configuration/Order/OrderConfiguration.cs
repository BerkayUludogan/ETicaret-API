using ETicaret.Application.Common.Constants.FieldLengths;
using ETicaret.Domain.Entities.Order;
using ETicaret.Domain.Enums;
using ETicaret.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.Persistence.Configuration.Order
{
    public class OrderConfiguration : BaseEntityConfiguration<OrderEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Orders", table =>
            {
                table.HasCheckConstraint("CK_Orders_TotalPrice_NotNegative", "[TotalPrice] >= 0");
            });

            builder.Property(x => x.Status)
                .IsRequired()
                .HasDefaultValue(OrderStatus.Pending);

            builder.Property(x => x.TotalPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.ShippingAddress)
                .IsRequired()
                .HasMaxLength(OrderFieldLengths.ShippingAddress);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
