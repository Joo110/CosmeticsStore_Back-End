using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public IEnumerable<OrderItemModel> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string TotalCurrency { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
