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
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Sku)
            .IsRequired()
            .HasMaxLength(100);
            builder.HasIndex(x => x.Sku).IsUnique();


            builder.Property(x => x.Title).HasMaxLength(200).IsRequired(false);


            builder.Property(x => x.PriceAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();


            builder.Property(x => x.PriceCurrency)
            .HasMaxLength(10)
            .IsRequired()
            .HasDefaultValue("EGP");


            builder.Property(x => x.StockQuantity).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(true);


            builder.HasOne(v => v.Product)
            .WithMany(p => p.Variants)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.ModifiedAtUtc).IsRequired(false);
        }
    }
}
