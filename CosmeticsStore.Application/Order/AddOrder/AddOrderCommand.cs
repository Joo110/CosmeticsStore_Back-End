using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Order.AddOrder
{
    public class AddOrderCommand : IRequest<OrderResponse>
    {
        public Guid UserId { get; set; }
        public Guid? ShippingAddressId { get; set; }
        public string Status { get; set; } = "Draft";

        // Optional: items to create with the order.
        public List<CreateOrderItemDto>? Items { get; set; }

        public decimal TotalAmount { get; set; }
        public string TotalCurrency { get; set; } = "EGP";

        // DTO for client to send order items (adjust fields to match your OrderItem entity)
        public class CreateOrderItemDto
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public string Currency { get; set; } = "EGP";
        }
    }
}
