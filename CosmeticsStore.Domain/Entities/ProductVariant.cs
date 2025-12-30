using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class ProductVariant : EntityBase, IAuditableEntity
    {
        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public string Sku { get; set; } = null!;
        public string? Title { get; set; }

        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; } = "EGP";
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }

        // Audit
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
