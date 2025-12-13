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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);


            builder.Property(x => x.Slug).HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.Description).HasMaxLength(4000).IsRequired(false);


            builder.Property(x => x.IsPublished).HasDefaultValue(false);


            builder.HasMany(p => p.Variants)
            .WithOne(v => v.Product)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Cascade);


            // Media.OwnerId is used as a polymorphic owner id; configure relationship without navigation on Media
            builder.HasMany(p => p.Media)
            .WithOne()
            .HasForeignKey("OwnerId")
            .HasPrincipalKey(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(p => p.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.ModifiedAtUtc).IsRequired(false);
        }
    }
}
