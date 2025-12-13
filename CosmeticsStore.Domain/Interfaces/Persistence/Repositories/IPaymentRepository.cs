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
    public interface IPaymentRepository
    {
        Task<bool> ExistsAsync(Expression<Func<Payment, bool>> predicate,
                               CancellationToken cancellationToken = default);

        Task<PaginatedList<PaymentModel>> GetForManagementAsync(Query<Payment> query,
            CancellationToken cancellationToken = default);

        Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Payment>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);

        Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken = default);

        Task UpdateAsync(Payment payment, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
