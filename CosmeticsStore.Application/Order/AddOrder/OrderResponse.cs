using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Order.AddOrder
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; } = default!;
        public Guid? ShippingAddressId { get; set; }

        public List<OrderItemResponse>? Items { get; set; }
        public List<PaymentResponse>? Payments { get; set; }

        public decimal TotalAmount { get; set; }
        public string TotalCurrency { get; set; } = "EGP";

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }

        public class OrderItemResponse
        {
            public Guid OrderItemId { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public string Currency { get; set; } = "EGP";
        }

        public class PaymentResponse
        {
            public Guid PaymentId { get; set; }
            public decimal Amount { get; set; }
            public string Currency { get; set; } = "EGP";
            public string Method { get; set; } = default!;
            public DateTime CreatedAtUtc { get; set; }
        }
    }
}
