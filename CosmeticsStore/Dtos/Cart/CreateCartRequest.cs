namespace CosmeticsStore.Dtos.Cart
{
    public class CreateCartRequest
    {
        public Guid UserId { get; set; }
        public List<CartItemRequest>? Items { get; set; }
    }
}
