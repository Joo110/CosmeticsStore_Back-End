using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Interfaces.Persistence.Repositories
{
    public interface IMediaRepository
    {
        Task<bool> ExistsAsync(Expression<Func<Media, bool>> predicate,
                               CancellationToken cancellationToken = default);
        Task<IEnumerable<Media>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);

        Task<Media?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Media> CreateAsync(Media media, CancellationToken cancellationToken = default);

        Task UpdateAsync(Media media, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Media?> GetPrimaryForOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default);

        Task<PaginatedList<Media>> GetForManagementAsync(Query<Media> query,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Media>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
