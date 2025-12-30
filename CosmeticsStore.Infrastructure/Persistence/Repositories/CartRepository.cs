using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Messages;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using CosmeticsStore.Infrastructure.Persistence.Extensions;
using Google;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CosmeticsStore.Infrastructure.Persistence.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Cart cart, CancellationToken cancellationToken)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
            // don't SaveChanges here if caller will call SaveChangesAsync (Create handler can call SaveChanges)
        }

        public async Task DeleteAsync(Cart cart, CancellationToken cancellationToken)
        {
            _context.Carts.Remove(cart);
            // caller should call SaveChangesAsync
            await Task.CompletedTask;
        }



        // Read-only, AsNoTracking for performance (used by queries)
        public async Task<Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken)
        {
            return await _context.Carts
                .AsNoTracking()
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);
        }

        public async Task AddItemAsync(CartItem item, CancellationToken cancellationToken = default)
        {
            await _context.CartItems.AddAsync(item, cancellationToken);
        }

        public async Task AddItemToCartAsync(Guid cartId, CartItem cartItem, CancellationToken cancellationToken)
        {
            // Check if item already exists
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId &&
                                           ci.ProductVariantId == cartItem.ProductVariantId,
                                           cancellationToken);

            if (existingItem != null)
            {
                // Update quantity
                existingItem.Quantity += cartItem.Quantity;
                existingItem.ModifiedAtUtc = DateTime.UtcNow;
            }
            else
            {
                // Add new item
                cartItem.CartId = cartId; // ⭐ Set CartId
                cartItem.ModifiedAtUtc = DateTime.UtcNow;
                await _context.CartItems.AddAsync(cartItem, cancellationToken);
            }

            await SaveChangesAsync(cancellationToken);
        }

        // Tracked version for updates (no AsNoTracking)
        public async Task<Cart?> GetByIdForUpdateAsync(Guid cartId, CancellationToken cancellationToken)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);
        }

        public async Task<Cart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Carts
                .AsNoTracking()
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        }

        public async Task RemoveItemAsync(Guid cartId, Guid itemId, CancellationToken ct)
        {
            // Get the cart with its items tracked
            var cart = await GetByIdForUpdateAsync(cartId, ct);
            if (cart == null)
                throw new CartNotFoundException("Cart not found.");

            // Find the item inside the cart
            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new CartItemNotFoundException("Item not found in cart.");

            // Remove the item from DbSet
            _context.CartItems.Remove(item);

            // Update cart's modified time for auditing
            cart.ModifiedAtUtc = DateTime.UtcNow;

            // Save changes
            await _context.SaveChangesAsync(ct);
        }


        public async Task<Cart?> GetByUserIdForUpdateAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        }
        public async Task UpdateAsync(Cart cart, CancellationToken ct)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync(ct);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
