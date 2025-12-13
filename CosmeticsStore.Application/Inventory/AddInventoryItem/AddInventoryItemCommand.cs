using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Inventory.AddInventoryItem
{
    public class AddInventoryItemCommand : IRequest<Guid>
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public AddInventoryItemCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
