using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class InventoryTransaction : EntityBase, IAuditableEntity
    {
        public Guid InventoryId { get; }
        public Guid ProductVariantId { get; set; }
        public virtual ProductVariant? ProductVariant { get; set; }

        public int QuantityChanged { get; set; }
        public string TransactionType { get; set; } = null!; // Sale, Restock, Adjustment, Reservation, Release
        public Guid? RelatedOrderId { get; set; }
        public string? Note { get; set; }

        // Audit
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
