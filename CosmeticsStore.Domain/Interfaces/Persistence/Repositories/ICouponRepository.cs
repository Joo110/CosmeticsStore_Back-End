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
    public interface ICouponRepository
    {
        Task<bool> ExistsAsync(Expression<Func<Coupon, bool>> predicate,
                               CancellationToken cancellationToken = default);

        Task<PaginatedList<CouponModel>> GetForManagementAsync(Query<Coupon> query,
            CancellationToken cancellationToken = default);

        Task<Coupon?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Coupon?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

        Task<Coupon> CreateAsync(Coupon coupon, CancellationToken cancellationToken = default);

        Task UpdateAsync(Coupon coupon, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
