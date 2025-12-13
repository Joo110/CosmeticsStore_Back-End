using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Models
{
    public class InventoryTransactionModel
    {
        public Guid Id { get; set; }
        public Guid ProductVariantId { get; set; }
        public int QuantityChanged { get; set; }
        public string TransactionType { get; set; }
        public Guid? RelatedOrderId { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
