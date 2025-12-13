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
    public interface IRoleRepository
    {
        Task<bool> ExistsAsync(Expression<Func<Role, bool>> predicate,
                               CancellationToken cancellationToken = default);

        Task<PaginatedList<Role>> GetForManagementAsync(Query<Role> query,
            CancellationToken cancellationToken = default);

        Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Role?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default);

        Task<Role> CreateAsync(Role role, CancellationToken cancellationToken = default);

        Task UpdateAsync(Role role, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
