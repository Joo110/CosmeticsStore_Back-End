using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class Payment : EntityBase, IAuditableEntity
    {
        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EGP";

        public string Status { get; set; } = "Pending"; // Pending, Succeeded, Failed, Refunded
        public string Provider { get; set; } = null!; // Stripe, PayPal ...
        public string? TransactionId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        // Audit
        public DateTime CreatedAtUtc { get => CreatedOnUtc; set => CreatedOnUtc = value; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
