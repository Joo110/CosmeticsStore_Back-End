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
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db) => _db = db;

        public async Task<bool> ExistsAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return await _db.Set<Order>().AnyAsync(predicate, cancellationToken);
        }

        public async Task<PaginatedList<OrderModel>> GetForManagementAsync(Query<Order> query, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _db.Set<Order>()
                .AsNoTracking()
                .Include(o => o.Items);

  
            // Pagination
            var totalCount = await queryable.CountAsync(cancellationToken);
            var items = await queryable
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(o => new OrderModel
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    TotalCurrency = o.TotalCurrency,
                    CreatedAtUtc = o.CreatedAtUtc
                })
                .ToListAsync(cancellationToken);

            return new PaginatedList<OrderModel>(items, totalCount, query.PageIndex, query.PageSize);
        }


        public async Task<List<OrderModel>> GetUserOrdersAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var ordersData = await _db.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.User)
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                .OrderByDescending(o => o.CreatedAtUtc)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = ordersData.Select(order => new OrderModel
            {
                Id = order.Id,
                UserName = order.User.FullName,
                CreatedAtUtc = order.CreatedAtUtc,
                TotalCurrency = order.Items.FirstOrDefault()?.UnitPriceCurrency ?? "EGP",
                Items = order.Items.Select(oi => new OrderItemModel
                {
                    Id = oi.Id,
                    ProductName = oi.ProductVariant.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPriceAmount = oi.UnitPriceAmount,
                    UnitPriceCurrency = oi.UnitPriceCurrency,
                    ItemTotalPrice = oi.Quantity * oi.UnitPriceAmount
                }).ToList()
            }).ToList();

            foreach (var order in result)
            {
                order.TotalAmount = order.Items.Sum(i => i.ItemTotalPrice);
            }

            return result;
        }

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Order>()
                .AsNoTracking()
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Order>()
                .AsNoTracking()
                .Where(o => o.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(order);
            _db.Set<Order>().Add(order);
            await _db.SaveChangesAsync(cancellationToken);
            return order;
        }

        public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(order);
            _db.Set<Order>().Update(order);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Set<Order>().FindAsync(new object[] { id }, cancellationToken);
            if (entity == null)
                throw new OrderNotFoundException($"Order with ID {id} not found.");

            _db.Set<Order>().Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetPendingPaymentOrdersAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Set<Order>()
                .AsNoTracking()
                .Where(o => o.Status == "PendingPayment")
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Order> ApplySorting(IQueryable<Order> queryable, string? sortBy, bool descending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return descending ? queryable.OrderByDescending(o => o.CreatedAtUtc) : queryable.OrderBy(o => o.CreatedAtUtc);

            return sortBy.ToLower() switch
            {
                "userid" => descending ? queryable.OrderByDescending(o => o.UserId) : queryable.OrderBy(o => o.UserId),
                "status" => descending ? queryable.OrderByDescending(o => o.Status) : queryable.OrderBy(o => o.Status),
                "totalamount" => descending ? queryable.OrderByDescending(o => o.TotalAmount) : queryable.OrderBy(o => o.TotalAmount),
                "createdat" => descending ? queryable.OrderByDescending(o => o.CreatedAtUtc) : queryable.OrderBy(o => o.CreatedAtUtc),
                _ => descending ? queryable.OrderByDescending(o => o.CreatedAtUtc) : queryable.OrderBy(o => o.CreatedAtUtc),
            };
        }
    }
}
