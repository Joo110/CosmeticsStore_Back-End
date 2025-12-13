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
    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext _db;
        public CouponRepository(AppDbContext db) => _db = db;

        public async Task<bool> ExistsAsync(Expression<Func<Coupon, bool>> predicate, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return await _db.Set<Coupon>().AnyAsync(predicate, cancellationToken);
        }

        public async Task<PaginatedList<CouponModel>> GetForManagementAsync(Query<Coupon> query, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _db.Set<Coupon>().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
                queryable = queryable.Where(c => c.Code.Contains(query.SearchTerm));

            queryable = ApplySorting(queryable, query.SortBy, query.SortDescending);

            var totalCount = await queryable.CountAsync(cancellationToken);
            var items = await queryable
                .GetPage(query.PageIndex, query.PageSize)
                .Select(c => new CouponModel
                {
                    Id = c.Id,
                    Code = c.Code,
                    DiscountPercentage = c.DiscountPercentage,
                    MaxDiscountAmount = c.MaxDiscountAmount,
                    ValidFromUtc = c.ValidFromUtc,
                    ValidUntilUtc = c.ValidUntilUtc,
                    UsageLimit = c.UsageLimit,
                    TimesUsed = c.TimesUsed,
                    IsActive = c.IsActive,
                    CreatedAtUtc = c.CreatedAtUtc
                })
                .ToListAsync(cancellationToken);

            return new PaginatedList<CouponModel>(items, totalCount, query.PageIndex, query.PageSize);
        }

        public async Task<Coupon?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Coupon>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Coupon?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Coupon>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Code == code, cancellationToken);
        }

        public async Task<Coupon> CreateAsync(Coupon coupon, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(coupon);
            _db.Set<Coupon>().Add(coupon);
            await _db.SaveChangesAsync(cancellationToken);
            return coupon;
        }

        public async Task UpdateAsync(Coupon coupon, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(coupon);
            _db.Set<Coupon>().Update(coupon);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var coupon = await _db.Set<Coupon>().FindAsync(new object[] { id }, cancellationToken);

            if (coupon == null)
                throw new CouponNotFoundException("Coupon not found.");

            _db.Set<Coupon>().Remove(coupon);
            await _db.SaveChangesAsync(cancellationToken);
        }

        private IQueryable<Coupon> ApplySorting(IQueryable<Coupon> queryable, string? sortBy, bool descending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return descending ? queryable.OrderByDescending(c => c.CreatedAtUtc) : queryable.OrderBy(c => c.CreatedAtUtc);

            return (sortBy.ToLower()) switch
            {
                "code" => descending ? queryable.OrderByDescending(c => c.Code) : queryable.OrderBy(c => c.Code),
                "createdat" => descending ? queryable.OrderByDescending(c => c.CreatedAtUtc) : queryable.OrderBy(c => c.CreatedAtUtc),
                _ => descending ? queryable.OrderByDescending(c => c.CreatedAtUtc) : queryable.OrderBy(c => c.CreatedAtUtc),
            };
        }
    }
}
