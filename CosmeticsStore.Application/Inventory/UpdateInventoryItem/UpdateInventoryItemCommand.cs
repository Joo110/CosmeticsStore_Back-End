using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Inventory.UpdateInventoryItem
{
    public class UpdateInventoryItemCommand : IRequest<Unit>
    {
        public Guid InventoryItemId { get; }
        public int Quantity { get; }
        public string? Location { get; }

        public UpdateInventoryItemCommand(Guid inventoryItemId, int quantity, string? location)
        {
            InventoryItemId = inventoryItemId;
            Quantity = quantity;
            Location = location;
        }
    }
}
