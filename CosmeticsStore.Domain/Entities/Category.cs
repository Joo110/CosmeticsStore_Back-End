using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class Category : EntityBase, IAuditableEntity
    {
        [Column("Id")]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Slug { get; set; }
        public string? Description { get; set; }

        // Navigation
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        // Audit
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
