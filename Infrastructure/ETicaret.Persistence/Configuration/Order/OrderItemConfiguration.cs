using ETicaret.Application.Common.Constants.FieldLengths;
using ETicaret.Domain.Entities.Order;
using ETicaret.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.Persistence.Configuration.Order
{
    public class OrderItemConfiguration : BaseEntityConfiguration<OrderItemEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("OrderItems", table =>
            {
                table.HasCheckConstraint("CK_OrderItems_Quantity_GreaterThanZero", "[Quantity] > 0");
                table.HasCheckConstraint("CK_OrderItems_UnitPrice_NotNegative", "[UnitPrice] >= 0");
                table.HasCheckConstraint("CK_OrderItems_TotalPrice_NotNegative", "[TotalPrice] >= 0");
            });

            builder.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(OrderFieldLengths.ProductName);

            builder.Property(x => x.ProductSku)
                .IsRequired()
                .HasMaxLength(OrderFieldLengths.ProductSku);

            builder.Property(x => x.UnitPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.TotalPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
