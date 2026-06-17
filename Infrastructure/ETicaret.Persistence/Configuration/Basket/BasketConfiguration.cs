using ETicaret.Domain.Entities.Basket;
using ETicaret.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace ETicaret.Persistence.Configuration.Basket
{
    public class BasketConfiguration : BaseEntityConfiguration<BasketEntity>
    {
        public override void Configure(EntityTypeBuilder<BasketEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("Baskets");

            builder.HasIndex(x=>x.UserId)
                .IsUnique();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
