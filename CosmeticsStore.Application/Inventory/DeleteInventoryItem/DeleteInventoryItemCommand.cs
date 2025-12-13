using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Inventory.DeleteInventoryItem
{
    public class DeleteInventoryItemCommand : IRequest<Unit>
    {
        public Guid InventoryItemId { get; }

        public DeleteInventoryItemCommand(Guid inventoryItemId)
        {
            InventoryItemId = inventoryItemId;
        }
    }
}
