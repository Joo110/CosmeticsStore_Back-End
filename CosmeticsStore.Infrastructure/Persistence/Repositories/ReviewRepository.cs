using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CosmeticsStore.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _db;
        public ReviewRepository(AppDbContext db) => _db = db;

        public async Task<bool> ExistsAsync(Expression<Func<Review, bool>> predicate, CancellationToken cancellationToken = default)
            => await _db.Set<Review>().AnyAsync(predicate, cancellationToken);

        public async Task<PaginatedList<ReviewModel>> GetForManagementAsync(Query<Review> query, CancellationToken cancellationToken = default)
        {
            var baseQ = _db.Set<Review>().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
                baseQ = baseQ.Where(r => r.Content.Contains(query.SearchTerm));

            var count = await baseQ.CountAsync(cancellationToken);

            var items = await baseQ
                .OrderByDescending(r => r.CreatedAtUtc)
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(r => new ReviewModel
                {
                    Id = r.Id,
                    ProductId = r.ProductId,
                    UserId = r.UserId,
                    Content = r.Content,
                    Rating = r.Rating,
                    IsApproved = r.IsApproved,
                    CreatedAtUtc = r.CreatedAtUtc
                }).ToListAsync(cancellationToken);

            return new PaginatedList<ReviewModel>(items, count, query.PageIndex, query.PageSize);
        }

        public async Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _db.Set<Review>().FindAsync(new object[] { id }, cancellationToken);

        public async Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
            => await _db.Set<Review>().Where(r => r.ProductId == productId).ToListAsync(cancellationToken);

        public async Task<Review> CreateAsync(Review review, CancellationToken cancellationToken = default)
        {
            _db.Set<Review>().Add(review);
            await _db.SaveChangesAsync(cancellationToken);
            return review;
        }

        public async Task UpdateAsync(Review review, CancellationToken cancellationToken = default)
        {
            _db.Set<Review>().Update(review);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Set<Review>().FindAsync(new object[] { id }, cancellationToken);
            if (entity == null) return;
            _db.Set<Review>().Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Review>> GetTopReviewsAsync(int count, CancellationToken cancellationToken = default)
            => await _db.Set<Review>().OrderByDescending(r => r.Rating).Take(count).ToListAsync(cancellationToken);
    }
}
