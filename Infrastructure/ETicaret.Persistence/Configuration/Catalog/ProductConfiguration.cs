using ETicaret.Application.Common.Constants.FieldLengths;
using ETicaret.Domain.Entities.Catalog;
using ETicaret.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.Persistence.Configuration.Catalog
{
    public class ProductConfiguration : BaseEntityConfiguration<ProductEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Products", table =>
            {
                table.HasCheckConstraint("CK_Products_Price_GreaterThanZero", "[Price] > 0");
                table.HasCheckConstraint("CK_Products_StockQuantity_NotNegative", "[StockQuantity] >= 0");
                table.HasCheckConstraint("CK_Products_DiscountPrice_Valid", "[DiscountPrice] IS NULL OR [DiscountPrice] >= 0");
            });

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(CatalogFieldLengths.ProductName);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(CatalogFieldLengths.ProductSlug);

            builder.HasIndex(x => x.Slug)
                .IsUnique();

            builder.Property(x => x.Description)
                .HasMaxLength(CatalogFieldLengths.ProductDescription);

            builder.Property(x => x.SKU)
                .IsRequired()
                .HasMaxLength(CatalogFieldLengths.ProductSku);

            builder.HasIndex(x => x.SKU)
                .IsUnique();

            builder.Property(x => x.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.DiscountPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.StockQuantity)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.IsFeatured)
                .HasDefaultValue(false);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
