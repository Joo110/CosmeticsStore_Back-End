using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class Cart : EntityBase, IAuditableEntity
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        // Navigation
        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        // Audit
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
