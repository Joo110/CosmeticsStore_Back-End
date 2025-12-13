using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CosmeticsStore.Infrastructure.Persistence.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly AppDbContext _db;
        public MediaRepository(AppDbContext db) => _db = db;

        public async Task<bool> ExistsAsync(Expression<Func<Media, bool>> predicate, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return await _db.Set<Media>().AnyAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<Media>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Media>()
                .AsNoTracking()
                .Where(m => m.OwnerId == ownerId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Media?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Media>()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        public async Task<Media> CreateAsync(Media media, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(media);
            _db.Set<Media>().Add(media);
            await _db.SaveChangesAsync(cancellationToken);
            return media;
        }

        public async Task UpdateAsync(Media media, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(media);
            _db.Set<Media>().Update(media);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Set<Media>().FindAsync(new object[] { id }, cancellationToken);
            if (entity == null)
                throw new MediaNotFoundException($"Media with ID {id} not found.");

            _db.Set<Media>().Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<Media?> GetPrimaryForOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Media>()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.OwnerId == ownerId && m.IsPrimary, cancellationToken);
        }

        public async Task<IEnumerable<Media>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Set<Media>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<PaginatedList<Media>> GetForManagementAsync(
     Query<Media> query,
     CancellationToken cancellationToken = default)
        {
            var baseQuery = _db.Set<Media>()
                .AsNoTracking()
                .AsQueryable();

            // --- Filtering (SearchTerm) ---
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                string term = query.SearchTerm.Trim().ToLower();

                baseQuery = baseQuery.Where(m =>
                    m.FileName.ToLower().Contains(term) ||
                    m.ContentType.ToLower().Contains(term) ||
                    m.Url.ToLower().Contains(term)
                );
            }

            // --- Count Before Pagination ---
            int count = await baseQuery.CountAsync(cancellationToken);

            // --- Sorting ---
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                baseQuery = query.SortBy.ToLower() switch
                {
                    "filename" => (query.SortDescending ? baseQuery.OrderByDescending(m => m.FileName) : baseQuery.OrderBy(m => m.FileName)),
                    "contenttype" => (query.SortDescending ? baseQuery.OrderByDescending(m => m.ContentType) : baseQuery.OrderBy(m => m.ContentType)),
                    "size" => (query.SortDescending ? baseQuery.OrderByDescending(m => m.SizeInBytes) : baseQuery.OrderBy(m => m.SizeInBytes)),
                    "createdat" => (query.SortDescending ? baseQuery.OrderByDescending(m => m.CreatedAtUtc) : baseQuery.OrderBy(m => m.CreatedAtUtc)),
                    _ => baseQuery.OrderBy(m => m.CreatedAtUtc)
                };
            }
            else
            {
                baseQuery = baseQuery.OrderBy(m => m.CreatedAtUtc); // default sorting
            }

            // --- Pagination ---
            var items = await baseQuery
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedList<Media>(items, count, query.PageIndex, query.PageSize);
        }

    }
}
