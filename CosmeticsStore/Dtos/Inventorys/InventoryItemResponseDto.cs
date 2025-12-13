namespace CosmeticsStore.Dtos.Inventorys
{
    public class InventoryItemResponseDto
    {
        public Guid InventoryItemId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Location { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
