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
    public class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            builder.ToTable("InventoryTransactions");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.QuantityChanged).IsRequired();
            builder.Property(x => x.TransactionType).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Note).HasMaxLength(1000).IsRequired(false);


            builder.HasOne(t => t.ProductVariant)
            .WithMany()
            .HasForeignKey(t => t.ProductVariantId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.ModifiedAtUtc).IsRequired(false);
        }
    }
}
