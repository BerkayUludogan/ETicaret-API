using ETicaret.Domain.Entities.Order;
using ETicaret.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.Persistence.Configuration.Order
{
    public class OrderStatusHistoryConfiguration : BaseEntityConfiguration<OrderStatusHistoryEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderStatusHistoryEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("OrderStatusHistories");

            builder.Property(x => x.OldStatus)
                .IsRequired();

            builder.Property(x => x.NewStatus)
                .IsRequired();

            builder.HasOne(x => x.Order)
                .WithMany(x => x.StatusHistories)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ChangedByUser)
                .WithMany()
                .HasForeignKey(x => x.ChangedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.OrderId);
        }
    }
}
