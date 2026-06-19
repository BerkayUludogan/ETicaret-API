using ETicaret.Application.Common.Constants.FieldLengths;
using ETicaret.Domain.Entities.Payment;
using ETicaret.Domain.Enums;
using ETicaret.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.Persistence.Configuration.Payment
{
    public class PaymentConfiguration : BaseEntityConfiguration<PaymentEntity>
    {
        public override void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Payments", table =>
            {
                table.HasCheckConstraint("CK_Payments_Amount_NotNegative", "[Amount] >= 0");
            });

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.PaymentMethod)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasDefaultValue(PaymentStatus.Pending);

            builder.Property(x => x.TransactionId)
                .IsRequired()
                .HasMaxLength(PaymentFieldLengths.TransactionId);

            builder.Property(x => x.FailedReason)
                .HasMaxLength(PaymentFieldLengths.FailedReason);

            builder.Property(x => x.PaidDate)
                .IsRequired(false);

            builder.HasOne(x => x.Order)
                .WithMany()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.OrderId);
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.TransactionId)
                .IsUnique();
        }
    }
}
