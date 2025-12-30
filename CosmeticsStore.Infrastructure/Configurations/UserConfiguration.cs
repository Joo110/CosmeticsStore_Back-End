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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);
            builder.HasIndex(x => x.Email).IsUnique();


            builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(200);


            builder.Property(x => x.PhoneNumber)
            .HasMaxLength(50);


            // Navigation rules
            builder.HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(u => u.Reviews)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(u => u.Carts)
       .WithOne() 
       .HasForeignKey(c => c.UserId)
       .OnDelete(DeleteBehavior.Cascade);

            // User ↔ Role (Many-to-Many)
            builder
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRoles",
                    j => j
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRoles");
                    });


            // Audit
            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.ModifiedAtUtc).IsRequired(false);
        }
    }
}
