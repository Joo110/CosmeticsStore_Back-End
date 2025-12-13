using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Inventory.AddInventoryItem
{
    public class InventoryItemResponse
    {
        public Guid InventoryItemId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Location { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
