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
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Street).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.City).HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.State).HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.Country).HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.PostalCode).HasMaxLength(50).IsRequired(false);


            builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.ModifiedAtUtc).IsRequired(false);
        }
    }
}
