using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using CosmeticsStore.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CosmeticsStore.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db) => _db = db;

        public async Task<bool> ExistsAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return await _db.Set<Product>().AnyAsync(predicate, cancellationToken);
        }

        public async Task<PaginatedList<ProductModel>> GetForManagementAsync(Query<Product> query, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _db.Set<Product>()
                .Include(p => p.Media)
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            // SEARCH
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                queryable = queryable.Where(p =>
                    p.Name.Contains(query.SearchTerm) ||
                    p.Description.Contains(query.SearchTerm)
                );
            }

            // SORT
            queryable = ApplySorting(queryable, query.SortBy, query.SortDescending);

            // PAGINATION
            var totalCount = await queryable.CountAsync(cancellationToken);
            var items = await queryable
                .GetPage(query.PageIndex, query.PageSize)
                .Select(p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    IsPublished = p.IsPublished,
                    CreatedAtUtc = p.CreatedAtUtc,
                    Media = p.Media.Select(m => new MediaModel
                    {
                        Id = m.Id,
                        OwnerId = m.OwnerId,
                        Url = m.Url,
                        FileName = m.FileName,
                        ContentType = m.ContentType,
                        SizeInBytes = m.SizeInBytes,
                        IsPrimary = m.IsPrimary,
                        CreatedAtUtc = m.CreatedAtUtc
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return new PaginatedList<ProductModel>(items, totalCount, query.PageIndex, query.PageSize);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _db.Set<Product>()
                .Include(p => p.Variants)
                .Include(p => p.Media)
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
            => await _db.Set<Product>()
                .AsNoTracking()
                .Include(p => p.Variants)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Slug == slug, cancellationToken);

        public async Task<ProductVariant?> GetVariantBySkuAsync(string sku, CancellationToken cancellationToken = default)
            => await _db.Set<ProductVariant>()
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Sku == sku, cancellationToken);

        public Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(product);
            _db.Set<Product>().Add(product);
            return Task.FromResult(product);
        }

        public Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(product);
            _db.Set<Product>().Update(product);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Set<Product>().FindAsync(new object[] { id }, cancellationToken);
            if (entity == null) return;

            _db.Set<Product>().Remove(entity);
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Product>()
                .AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetTopRatedAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Product>()
                .AsNoTracking()
                .OrderByDescending(p => p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0)
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Product> ApplySorting(IQueryable<Product> queryable, string? sortBy, bool descending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return descending ? queryable.OrderByDescending(p => p.CreatedAtUtc) : queryable.OrderBy(p => p.CreatedAtUtc);

            return sortBy.ToLower() switch
            {
                "name" => descending ? queryable.OrderByDescending(p => p.Name) : queryable.OrderBy(p => p.Name),
                "createdat" => descending ? queryable.OrderByDescending(p => p.CreatedAtUtc) : queryable.OrderBy(p => p.CreatedAtUtc),
                "category" => descending ? queryable.OrderByDescending(p => p.Category.Name) : queryable.OrderBy(p => p.Category.Name),
                _ => descending ? queryable.OrderByDescending(p => p.CreatedAtUtc) : queryable.OrderBy(p => p.CreatedAtUtc),
            };
        }
    }
}
