using ETicaret.Domain.Entities.Basket;
using ETicaret.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.Persistence.Configuration.Basket
{
   public class BasketItemConfiguration : BaseEntityConfiguration<BasketItemEntity>
    {
        public override void Configure(EntityTypeBuilder<BasketItemEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("BasketItems",table=>
            {
                table.HasCheckConstraint("CK_BasketItems_Quantity_GreaterThanZero", "[Quantity] > 0");
            });
            
            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.HasIndex(x => new { x.BasketId, x.ProductId }).IsUnique();

            builder.HasOne(x=>x.Basket)
                .WithMany(x=>x.Items)
                .HasForeignKey(x=>x.BasketId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
