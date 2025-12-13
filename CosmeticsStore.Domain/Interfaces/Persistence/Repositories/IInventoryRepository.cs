using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Interfaces.Persistence.Repositories
{
    public interface IInventoryRepository
    {
        Task<bool> ExistsAsync(Expression<Func<InventoryTransaction, bool>> predicate,
                               CancellationToken cancellationToken = default);

        Task<PaginatedList<InventoryTransactionModel>> GetForManagementAsync(Query<InventoryTransaction> query,
            CancellationToken cancellationToken = default);

        Task<InventoryTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<InventoryTransaction> CreateTransactionAsync(InventoryTransaction tx, CancellationToken cancellationToken = default);

        Task AdjustStockAsync(Guid productVariantId, int delta, CancellationToken cancellationToken = default);

        Task<int> GetStockForVariantAsync(Guid productVariantId, CancellationToken cancellationToken = default);

        Task<IEnumerable<InventoryTransaction>> GetTransactionsForVariantAsync(Guid productVariantId, CancellationToken cancellationToken = default);
    }
}
