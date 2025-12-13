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
    public interface IOrderRepository
    {
        Task<bool> ExistsAsync(Expression<Func<Order, bool>> predicate,
                               CancellationToken cancellationToken = default);

        Task<PaginatedList<OrderModel>> GetForManagementAsync(Query<Order> query,
            CancellationToken cancellationToken = default);

        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default);

        Task UpdateAsync(Order order, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Order>> GetPendingPaymentOrdersAsync(CancellationToken cancellationToken = default);
    }
}
