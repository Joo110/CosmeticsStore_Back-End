using CosmeticsStore.Domain.Entities;
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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db) => _db = db;

        public async Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _db.Set<User>().AnyAsync(predicate, cancellationToken);
        }

        public async Task<PaginatedList<UserModel>> GetForManagementAsync(Query<User> query, CancellationToken cancellationToken = default)
        {
            var baseQ = _db.Set<User>()
                .Include(u => u.Roles)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
                baseQ = baseQ.Where(u => u.FullName.Contains(query.SearchTerm) || u.Email.Contains(query.SearchTerm));

            var count = await baseQ.CountAsync(cancellationToken);

            var items = await baseQ
                .OrderBy(u => u.FullName)
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    CreatedAtUtc = u.CreatedAtUtc,
                    Roles = u.Roles.Select(r => r.Name).ToList()
                })
                .ToListAsync(cancellationToken);

            return new PaginatedList<UserModel>(items, count, query.PageIndex, query.PageSize);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<User>()
                .Include(u => u.Roles)
                //.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _db.Set<User>()
                .Include(u => u.Roles)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            _db.Set<User>().Add(user);
            await _db.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _db.Set<User>().Update(user);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Set<User>().FindAsync(new object[] { id }, cancellationToken);
            if (entity == null) return;

            _db.Set<User>().Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}