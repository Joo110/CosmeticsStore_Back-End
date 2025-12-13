using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using CosmeticsStore.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CosmeticsStore.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db) => _db = db;

        public async Task<bool> ExistsAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return await _db.Set<Category>().AnyAsync(predicate, cancellationToken);
        }

        public async Task<PaginatedList<CategoryModel>> GetForManagementAsync(Query<Category> query, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _db.Set<Category>().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
                queryable = queryable.Where(c => c.Name.Contains(query.SearchTerm));

            queryable = ApplySorting(queryable, query.SortBy, query.SortDescending);

            var totalCount = await queryable.CountAsync(cancellationToken);
            var items = await queryable
                .GetPage(query.PageIndex, query.PageSize)
                .Select(c => new CategoryModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                    Description = c.Description,
                    CreatedAtUtc = c.CreatedAtUtc
                })
                .ToListAsync(cancellationToken);

            return new PaginatedList<CategoryModel>(items, totalCount, query.PageIndex, query.PageSize);
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Category>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(category);
            _db.Set<Category>().Add(category);
            await _db.SaveChangesAsync(cancellationToken);
            return category;
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(category);
            _db.Set<Category>().Update(category);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _db.Set<Category>().FindAsync(new object[] { id }, cancellationToken);
            if (category == null)
                throw new CategoryNotFoundException("Category not found.");

            _db.Set<Category>().Remove(category);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetWithProductsAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Set<Category>()
                .Include(c => c.Products)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Category> ApplySorting(IQueryable<Category> queryable, string? sortBy, bool descending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return queryable.OrderBy(c => c.Name);

            return (sortBy.ToLower()) switch
            {
                "name" => descending ? queryable.OrderByDescending(c => c.Name) : queryable.OrderBy(c => c.Name),
                "createdat" => descending ? queryable.OrderByDescending(c => c.CreatedAtUtc) : queryable.OrderBy(c => c.CreatedAtUtc),
                _ => queryable.OrderBy(c => c.Name),
            };
        }
    }
}
