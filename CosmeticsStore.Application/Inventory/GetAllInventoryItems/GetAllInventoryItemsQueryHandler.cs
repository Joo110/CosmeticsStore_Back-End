using CosmeticsStore.Application.Inventory.AddInventoryItem;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Inventory.GetAllInventoryItems
{
    public class GetInventoryItemByIdQueryHandler : IRequestHandler<GetInventoryItemByIdQuery, InventoryItemResponse>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public GetInventoryItemByIdQueryHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<InventoryItemResponse> Handle(GetInventoryItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _inventoryRepository.GetByIdAsync(request.InventoryItemId, cancellationToken);
            if (item == null) throw new InventoryTransactionNotFoundException("Inventory item not found.");

            return new InventoryItemResponse
            {
                InventoryItemId = item.InventoryId,
                ProductId = item.ProductVariantId,
                Quantity = item.QuantityChanged,
                CreatedAtUtc = item.CreatedAtUtc,
                ModifiedAtUtc = item.ModifiedAtUtc
            };
        }
    }
}
