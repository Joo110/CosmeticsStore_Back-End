using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Messages;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using CosmeticsStore.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CosmeticsStore.Infrastructure.Persistence.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _db;

        public CartRepository(AppDbContext db)
        {
            ArgumentNullException.ThrowIfNull(db);
            _db = db;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Cart, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return await _db.Carts.AnyAsync(predicate, cancellationToken);
        }

        public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Carts
                .Include(c => c.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Cart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _db.Carts
                .Include(c => c.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        }

        public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(cart);
            var entity = await _db.Carts.AddAsync(cart, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return entity.Entity;
        }

        public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(cart);
            _db.Carts.Update(cart);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cart = await _db.Carts.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (cart == null)
                throw new CartNotFoundException();

            _db.Carts.Remove(cart);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task AddItemAsync(CartItem item, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(item);
            await _db.CartItems.AddAsync(item, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveItemAsync(Guid cartItemId, CancellationToken cancellationToken = default)
        {
            var cartItem = await _db.CartItems.FirstOrDefaultAsync(i => i.Id == cartItemId, cancellationToken);

            if (cartItem == null)
                throw new CartItemNotFoundException();

            _db.CartItems.Remove(cartItem);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task ClearCartAsync(Guid cartId, CancellationToken cancellationToken = default)
        {
            var items = await _db.CartItems
                .Where(i => i.CartId == cartId)
                .ToListAsync(cancellationToken);

            _db.CartItems.RemoveRange(items);
            await _db.SaveChangesAsync(cancellationToken);
        }

        // ---------------------------
        //     PAGINATION + SORTING + SEARCH
        // ---------------------------
        public async Task<PaginatedList<Cart>> GetAsync(Query<Cart> query,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _db.Carts
                .Include(c => c.Items)
                .AsNoTracking()
                .AsQueryable();

            // SEARCH
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                queryable = queryable.Where(c =>
                    c.UserId.ToString().Contains(query.SearchTerm) ||
                    c.Id.ToString().Contains(query.SearchTerm));
            }

            // SORT
            queryable = ApplySorting(queryable, query.SortBy, query.SortDescending);

            // PAGINATION
            var totalCount = await queryable.CountAsync(cancellationToken);
            var items = await queryable
                .GetPage(query.PageIndex, query.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedList<Cart>(items, totalCount, query.PageIndex, query.PageSize);
        }

        private IQueryable<Cart> ApplySorting(IQueryable<Cart> queryable, string? sortBy, bool descending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return queryable.OrderBy(c => c.Id);

            return (sortBy.ToLower()) switch
            {
                "userid" => descending ? queryable.OrderByDescending(c => c.UserId) : queryable.OrderBy(c => c.UserId),
                "createdat" => descending ? queryable.OrderByDescending(c => c.CreatedAtUtc) : queryable.OrderBy(c => c.CreatedAtUtc),
                _ => descending ? queryable.OrderByDescending(c => c.Id) : queryable.OrderBy(c => c.Id),
            };
        }
    }
}
