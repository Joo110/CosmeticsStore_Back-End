using System;
using System.Collections.Generic;

namespace CosmeticsStore.Domain.Entities
{
    public class User : EntityBase, IAuditableEntity
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        // للـ Authentication
        public string? PasswordHash { get; set; }

        // Navigation
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<Role> Roles { get; set; } = new List<Role>();

        // Audit
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsEmailConfirmed { get; set; } = false;
    }
}
