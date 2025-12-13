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
        Task<bool> ExistsAsync(Expression<Func<Cart, bool>> predicate,
                               CancellationToken cancellationToken = default);

        Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Cart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default);

        Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddItemAsync(CartItem item, CancellationToken cancellationToken = default);

        Task RemoveItemAsync(Guid cartItemId, CancellationToken cancellationToken = default);

        Task ClearCartAsync(Guid cartId, CancellationToken cancellationToken = default);
    }
}
