using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Inventory.DeleteInventoryItem
{
    public class DeleteInventoryItemCommandHandler : IRequestHandler<DeleteInventoryItemCommand, Unit>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public DeleteInventoryItemCommandHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Unit> Handle(DeleteInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _inventoryRepository.GetByIdAsync(request.InventoryItemId, cancellationToken);
            if (item == null) throw new InventoryTransactionNotFoundException("Inventory item not found.");

            //await _inventoryRepository.DeleteAsync(request.InventoryItemId, cancellationToken);
            return Unit.Value;
        }
    }
}
