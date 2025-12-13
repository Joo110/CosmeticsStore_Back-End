using CosmeticsStore.Application.Inventory.AddInventoryItem;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Inventory.GetInventoryItemById
{
    public class GetInventoryItemByIdQuery : IRequest<InventoryItemResponse>
    {
        public Guid InventoryItemId { get; }

        public GetInventoryItemByIdQuery(Guid inventoryItemId) => InventoryItemId = inventoryItemId;
    }
}
