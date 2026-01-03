using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class Media : EntityBase, IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long SizeInBytes { get; set; }
        public bool IsPrimary { get; set; }

        // Audit
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
