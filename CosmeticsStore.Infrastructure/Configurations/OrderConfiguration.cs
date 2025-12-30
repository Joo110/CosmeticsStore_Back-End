using CosmeticsStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CosmeticsStore.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);

            // ======================
            // Basic properties
            // ======================
            builder.Property(x => x.Status)
                .HasMaxLength(50)
                .IsRequired()
                .HasDefaultValue("Draft");

            builder.Property(x => x.ShippingAddress)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            // ======================
            // Relationships
            // ======================
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ======================
            // Money
            // ======================
            builder.Property(x => x.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TotalCurrency)
                .HasMaxLength(10)
                .HasDefaultValue("EGP")
                .IsRequired();

            // ======================
            // Audit
            // ======================
            builder.Property(x => x.CreatedAtUtc)
                .IsRequired();

            builder.Property(x => x.ModifiedAtUtc)
                .IsRequired(false);
        }
    }
}
