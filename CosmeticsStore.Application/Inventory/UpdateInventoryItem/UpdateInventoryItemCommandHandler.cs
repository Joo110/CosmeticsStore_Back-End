using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Inventory.UpdateInventoryItem
{
    public class UpdateInventoryItemCommandHandler : IRequestHandler<UpdateInventoryItemCommand, Unit>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public UpdateInventoryItemCommandHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Unit> Handle(UpdateInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _inventoryRepository.GetByIdAsync(request.InventoryItemId, cancellationToken);
            if (item == null) throw new InventoryTransactionNotFoundException("Inventory item not found.");

            item.QuantityChanged = request.Quantity;
            item.ModifiedAtUtc = DateTime.UtcNow;

            //معلقه حاليا
            //await _inventoryRepository.UpdateAsync(item, cancellationToken);
            return Unit.Value;
        }
    }
}
