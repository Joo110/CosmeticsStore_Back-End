using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using CosmeticsStore.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CosmeticsStore.Infrastructure.Persistence.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _db;
        public InventoryRepository(AppDbContext db) => _db = db;

        public async Task<bool> ExistsAsync(Expression<Func<InventoryTransaction, bool>> predicate, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return await _db.Set<InventoryTransaction>().AnyAsync(predicate, cancellationToken);
        }

        public async Task<PaginatedList<InventoryTransactionModel>> GetForManagementAsync(Query<InventoryTransaction> query, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _db.Set<InventoryTransaction>().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                queryable = queryable.Where(t =>
                    t.ProductVariantId.ToString().Contains(query.SearchTerm) ||
                    (t.RelatedOrderId != null && t.RelatedOrderId.ToString().Contains(query.SearchTerm)) ||
                    t.TransactionType.Contains(query.SearchTerm) ||
                    t.Note.Contains(query.SearchTerm));
            }

            queryable = ApplySorting(queryable, query.SortBy, query.SortDescending);

            var totalCount = await queryable.CountAsync(cancellationToken);
            var items = await queryable
                .GetPage(query.PageIndex, query.PageSize)
                .Select(t => new InventoryTransactionModel
                {
                    Id = t.Id,
                    ProductVariantId = t.ProductVariantId,
                    QuantityChanged = t.QuantityChanged,
                    TransactionType = t.TransactionType,
                    RelatedOrderId = t.RelatedOrderId,
                    Note = t.Note,
                    CreatedAtUtc = t.CreatedAtUtc
                })
                .ToListAsync(cancellationToken);

            return new PaginatedList<InventoryTransactionModel>(items, totalCount, query.PageIndex, query.PageSize);
        }

        public async Task<InventoryTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<InventoryTransaction>()
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<InventoryTransaction> CreateTransactionAsync(InventoryTransaction tx, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(tx);
            _db.Set<InventoryTransaction>().Add(tx);
            await _db.SaveChangesAsync(cancellationToken);
            return tx;
        }

        public async Task AdjustStockAsync(Guid productVariantId, int delta, CancellationToken cancellationToken = default)
        {
            var variant = await _db.Set<ProductVariant>().FirstOrDefaultAsync(v => v.Id == productVariantId, cancellationToken);
            if (variant == null)
                throw new InventoryTransactionNotFoundException($"Product variant with ID {productVariantId} not found.");

            variant.StockQuantity += delta;

            var tx = new InventoryTransaction
            {
                ProductVariantId = productVariantId,
                QuantityChanged = delta,
                TransactionType = delta > 0 ? "Increase" : "Decrease",
                Note = "Adjustment",
                CreatedAtUtc = DateTime.UtcNow
            };

            _db.Set<InventoryTransaction>().Add(tx);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> GetStockForVariantAsync(Guid productVariantId, CancellationToken cancellationToken = default)
        {
            var variant = await _db.Set<ProductVariant>()
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == productVariantId, cancellationToken);

            return variant?.StockQuantity ?? 0;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetTransactionsForVariantAsync(Guid productVariantId, CancellationToken cancellationToken = default)
        {
            return await _db.Set<InventoryTransaction>()
                .AsNoTracking()
                .Where(t => t.ProductVariantId == productVariantId)
                .ToListAsync(cancellationToken);
        }

        private IQueryable<InventoryTransaction> ApplySorting(IQueryable<InventoryTransaction> queryable, string? sortBy, bool descending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return descending ? queryable.OrderByDescending(t => t.CreatedAtUtc) : queryable.OrderBy(t => t.CreatedAtUtc);

            return sortBy.ToLower() switch
            {
                "productvariantid" => descending ? queryable.OrderByDescending(t => t.ProductVariantId) : queryable.OrderBy(t => t.ProductVariantId),
                "quantitychanged" => descending ? queryable.OrderByDescending(t => t.QuantityChanged) : queryable.OrderBy(t => t.QuantityChanged),
                "transactiontype" => descending ? queryable.OrderByDescending(t => t.TransactionType) : queryable.OrderBy(t => t.TransactionType),
                "createdat" => descending ? queryable.OrderByDescending(t => t.CreatedAtUtc) : queryable.OrderBy(t => t.CreatedAtUtc),
                _ => descending ? queryable.OrderByDescending(t => t.CreatedAtUtc) : queryable.OrderBy(t => t.CreatedAtUtc),
            };
        }
    }
}
