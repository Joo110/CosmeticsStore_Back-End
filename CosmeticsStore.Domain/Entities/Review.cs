using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class Review : EntityBase, IAuditableEntity
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; } // 1..5
        public bool IsApproved { get; set; }

        // Audit
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
