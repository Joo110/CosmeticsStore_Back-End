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
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate,
                               CancellationToken cancellationToken = default);

        Task<PaginatedList<UserModel>> GetForManagementAsync(Query<User> query,
            CancellationToken cancellationToken = default);

        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);

        Task UpdateAsync(User user, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
