using ETicaret.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.Persistence.Configuration.Common
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnType("bit");

            builder.Property(x => x.CreatedDate)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.ModifiedDate)
                .HasColumnType("datetime2")
                .IsRequired(false);

        }
    }
}
