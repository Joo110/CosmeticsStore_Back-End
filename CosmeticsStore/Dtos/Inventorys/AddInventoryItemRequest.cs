namespace CosmeticsStore.Dtos.Inventorys
{
    public class AddInventoryItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
 