namespace CosmeticsStore.Dtos.Cart
{
    public class CreateCartRequest
    {
        // optional: you can remove UserId here and rely on JWT
        public Guid? UserId { get; set; }
        public List<CartItemRequest>? Items { get; set; }

    }

    // Add item request uses cartId in route
    //public class AddCartItemRequest
    //{
    //    public Guid ProductVariantId { get; set; }
    //    public int Quantity { get; set; }
    //}

    // Update quantity
    public class UpdateCartItemRequest
    {
        public int Quantity { get; set; }
    }

    // Response DTOs
    public class CartResponse
    {
        public Guid Id { get; set; } // بدل CartId
        public Guid UserId { get; set; }
        public List<CartItemResponse> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "EGP";
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }

    public class CartItemResponse
    {
        public Guid ItemId { get; set; }
        public Guid ProductVariantId { get; set; }
        public string? Title { get; set; }
        public decimal UnitPriceAmount { get; set; }
        public string UnitPriceCurrency { get; set; } = "EGP";
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }
}
