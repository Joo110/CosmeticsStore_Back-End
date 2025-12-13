using CosmeticsStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Infrastructure.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Currency).HasMaxLength(10).HasDefaultValue("EGP");
            builder.Property(x => x.Status).HasMaxLength(50).IsRequired().HasDefaultValue("Pending");
            builder.Property(x => x.Provider).HasMaxLength(150).IsRequired();
            builder.Property(x => x.TransactionId).HasMaxLength(250).IsRequired(false);


            // Avoid mapping the CreatedAtUtc wrapper property (it forwards to CreatedOnUtc)
            builder.Ignore(p => p.CreatedAtUtc);
            builder.Property(p => p.CreatedOnUtc).IsRequired();


            builder.HasOne(p => p.Order)
            .WithMany(o => o.Payments)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.Property(x => x.ModifiedAtUtc).IsRequired(false);
        }
    }
}
