using ETicaret.Application.Common.Constants.FieldLengths;
using ETicaret.Domain.Entities.Catalog;
using ETicaret.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.Persistence.Configuration.Catalog
{
    public class CategoryConfiguration : BaseEntityConfiguration<CategoryEntity>
    {
        public override void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Categories");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(CatalogFieldLengths.CategoryName);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(CatalogFieldLengths.CategorySlug);

            builder.HasIndex(x => x.Slug)
                .IsUnique();

            builder.Property(x => x.Description)
                .HasMaxLength(CatalogFieldLengths.CategoryDescription);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(x => x.ParentCategory)
                .WithMany(x => x.SubCategories)
                .HasForeignKey(x => x.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
