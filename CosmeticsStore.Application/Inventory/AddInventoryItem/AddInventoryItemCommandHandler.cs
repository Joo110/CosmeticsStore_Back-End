using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Inventory.AddInventoryItem
{
    public class AddInventoryItemCommandHandler : IRequestHandler<AddInventoryItemCommand, Guid>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public AddInventoryItemCommandHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Guid> Handle(AddInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Domain.Entities.InventoryTransaction
            {
                Id = Guid.NewGuid(),
                ProductVariantId = request.ProductId,
                QuantityChanged = request.Quantity,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _inventoryRepository.CreateTransactionAsync(item, cancellationToken);
            return item.Id;
        }
    }
}
