using CosmeticsStore.Application.Order.AddOrder;
using MediatR;

namespace CosmeticsStore.Application.Order.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<OrderResponse>
    {
        public Guid OrderId { get; set; }

        public string? Status { get; set; }

        public string? ShippingAddress { get; set; }
        public string? PhoneNumber { get; set; }

        public List<UpdateOrderItemDto>? Items { get; set; }

        public decimal? TotalAmount { get; set; }
        public string? TotalCurrency { get; set; }

        public class UpdateOrderItemDto
        {
            public Guid? OrderItemId { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public string Currency { get; set; } = "EGP";
        }
    }
}
