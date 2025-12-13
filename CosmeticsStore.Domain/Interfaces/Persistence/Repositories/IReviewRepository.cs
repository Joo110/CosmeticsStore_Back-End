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
    public interface IReviewRepository
    {
        Task<bool> ExistsAsync(Expression<Func<Review, bool>> predicate,
                               CancellationToken cancellationToken = default);

        Task<PaginatedList<ReviewModel>> GetForManagementAsync(Query<Review> query,
            CancellationToken cancellationToken = default);

        Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<Review> CreateAsync(Review review, CancellationToken cancellationToken = default);

        Task UpdateAsync(Review review, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Review>> GetTopReviewsAsync(int count, CancellationToken cancellationToken = default);
    }
}
