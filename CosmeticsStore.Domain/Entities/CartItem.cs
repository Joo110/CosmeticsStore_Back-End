using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class CartItem : EntityBase
    {
        public Guid CartId { get; set; }
        public virtual Cart? Cart { get; set; }

        public Guid ProductVariantId { get; set; }
        public virtual ProductVariant? ProductVariant { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPriceAmount { get; set; }
        public string UnitPriceCurrency { get; set; } = "EGP";
        public string? Title { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }

        public decimal LineTotal => UnitPriceAmount * Quantity;

    }
}
