using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _db;

        public RoleRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Role, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Role>().AnyAsync(predicate, cancellationToken);
        }

        public async Task<PaginatedList<Role>> GetForManagementAsync(Query<Role> query, CancellationToken cancellationToken = default)
        {
            var baseQuery = _db.Set<Role>().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                baseQuery = baseQuery.Where(r => r.Name.Contains(query.SearchTerm));
            }

            var count = await baseQuery.CountAsync(cancellationToken);

            var items = await baseQuery
                .OrderBy(r => r.Name)
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedList<Role>(items, count, query.PageIndex, query.PageSize);
        }

        public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Role>()
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Role?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Role>()
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
        }

        public async Task<Role> CreateAsync(Role role, CancellationToken cancellationToken = default)
        {
            _db.Set<Role>().Add(role);
            await _db.SaveChangesAsync(cancellationToken);
            return role;
        }

        public async Task UpdateAsync(Role role, CancellationToken cancellationToken = default)
        {
            _db.Set<Role>().Update(role);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Set<Role>().FindAsync(new object[] { id }, cancellationToken);
            if (entity == null) return;

            _db.Set<Role>().Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Set<Role>().AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
