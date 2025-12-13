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
    public interface ICategoryRepository
    {
        Task<bool> ExistsAsync(Expression<Func<Category, bool>> predicate,
                               CancellationToken cancellationToken = default);

        Task<PaginatedList<CategoryModel>> GetForManagementAsync(Query<Category> query,
            CancellationToken cancellationToken = default);

        Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default);

        Task UpdateAsync(Category category, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Category>> GetWithProductsAsync(CancellationToken cancellationToken = default);
    }
}
