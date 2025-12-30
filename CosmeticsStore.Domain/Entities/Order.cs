using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class Order : EntityBase, IAuditableEntity
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        public string Status { get; set; } = "Draft"; // Draft, PendingPayment, Paid, Processing, Shipped, Completed, Cancelled
        public string ShippingAddress { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;


        // Navigation
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public decimal TotalAmount { get; set; }
        public string TotalCurrency { get; set; } = "EGP";

        // Audit
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
