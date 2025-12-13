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
    public interface IProductRepository
    {
        Task<bool> ExistsAsync(Expression<Func<Product, bool>> predicate,
                               CancellationToken cancellationToken = default);

        Task<PaginatedList<ProductModel>> GetForManagementAsync(Query<Product> query,
            CancellationToken cancellationToken = default);

        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

        Task<ProductVariant?> GetVariantBySkuAsync(string sku, CancellationToken cancellationToken = default);

        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

        Task UpdateAsync(Product product, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);

        Task<IEnumerable<Product>> GetTopRatedAsync(int count, CancellationToken cancellationToken = default);
    }
}
