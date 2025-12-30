namespace CosmeticsStore.Dtos.Cart
{
    public class AddCartItemRequest
    {
        public Guid UserId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; } = 1;

    }
}
