namespace CosmeticsStore.Dtos.Order
{
    public class AddOrderRequest
    {
        public Guid UserId { get; set; }
        public Guid? ShippingAddressId { get; set; }
        public string Status { get; set; } = "Draft";
        public List<CreateOrderItemDto>? Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string TotalCurrency { get; set; } = "EGP";

        public class CreateOrderItemDto
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public string Currency { get; set; } = "EGP";
        }
    }

    public class UpdateOrderRequest
    {
        // properties that map to UpdateOrderCommand
        public string? Status { get; set; }
        public Guid? ShippingAddressId { get; set; }
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

    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; } = default!;
        public Guid? ShippingAddressId { get; set; }

        public List<OrderItemResponseDto>? Items { get; set; }
        public List<PaymentResponseDto>? Payments { get; set; }

        public decimal TotalAmount { get; set; }
        public string TotalCurrency { get; set; } = "EGP";

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }

        public class OrderItemResponseDto
        {
            public Guid OrderItemId { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public string Currency { get; set; } = "EGP";
        }

        public class PaymentResponseDto
        {
            public Guid PaymentId { get; set; }
            public decimal Amount { get; set; }
            public string Currency { get; set; } = "EGP";
            public string Method { get; set; } = default!;
            public DateTime CreatedAtUtc { get; set; }
        }
    }
}
