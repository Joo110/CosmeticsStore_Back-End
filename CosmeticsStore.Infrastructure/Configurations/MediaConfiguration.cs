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
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("Media");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.OwnerId).IsRequired();


            builder.Property(x => x.Url).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.FileName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ContentType).IsRequired().HasMaxLength(150);
            builder.Property(x => x.SizeInBytes).IsRequired();
            builder.Property(x => x.IsPrimary).HasDefaultValue(false);


            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.ModifiedAtUtc).IsRequired(false);
        }
    }
}
