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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");



            builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);


            builder.Property(x => x.Slug).HasMaxLength(150).IsRequired(false);
            builder.Property(x => x.Description).HasMaxLength(2000).IsRequired(false);

            builder.HasMany(c => c.Products)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.ModifiedAtUtc).IsRequired(false);
        }
    }
}
