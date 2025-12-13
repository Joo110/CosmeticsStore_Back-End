using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class Product : EntityBase, IAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public bool IsPublished { get; set; }

        // Navigation
        public virtual ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public virtual ICollection<Media> Media { get; set; } = new List<Media>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        // Audit
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
