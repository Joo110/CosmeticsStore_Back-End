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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Content).HasMaxLength(4000).IsRequired();
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.IsApproved).HasDefaultValue(false);


            builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Restrict);


            // Ensure rating between 1 and 5
            builder.HasCheckConstraint("CK_Reviews_Rating_Range", "Rating >= 1 AND Rating <= 5");


            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.ModifiedAtUtc).IsRequired(false);
        }
    }
}
