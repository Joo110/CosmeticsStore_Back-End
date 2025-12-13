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
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Coupons");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => x.Code).IsUnique();


            builder.Property(x => x.DiscountPercentage).HasColumnType("decimal(5,2)").IsRequired();
            builder.Property(x => x.MaxDiscountAmount).HasColumnType("decimal(18,2)").IsRequired(false);


            builder.Property(x => x.ValidFromUtc).IsRequired();
            builder.Property(x => x.ValidUntilUtc).IsRequired();


            builder.Property(x => x.UsageLimit).IsRequired();
            builder.Property(x => x.TimesUsed).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(true);


            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.ModifiedAtUtc).IsRequired(false);
        }
    }
}
