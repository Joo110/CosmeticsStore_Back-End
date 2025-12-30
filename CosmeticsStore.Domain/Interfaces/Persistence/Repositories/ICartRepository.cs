using CosmeticsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Interfaces.Persistence.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken);
        Task<Cart?> GetByIdForUpdateAsync(Guid cartId, CancellationToken cancellationToken);

        Task<Cart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<Cart?> GetByUserIdForUpdateAsync(Guid userId, CancellationToken cancellationToken);
        Task AddItemAsync(CartItem item, CancellationToken cancellationToken = default);
        Task CreateAsync(Cart cart, CancellationToken cancellationToken);
        Task DeleteAsync(Cart cart, CancellationToken cancellationToken);
        Task RemoveItemAsync(Guid cartId, Guid itemId, CancellationToken cancellationToken);
        Task AddItemToCartAsync(Guid cartId, CartItem item, CancellationToken cancellationToken = default); // ✅
        Task UpdateAsync(Cart cart, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
