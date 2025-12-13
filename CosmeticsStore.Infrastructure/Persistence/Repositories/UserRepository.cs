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
    public class UserRepository : IUserRepository // using existing interface name: ICustomerRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) => _db = db;

        public async Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
        {
            // if you use User entity, adapt predicate or change interface to accept User
            // fallback: perform cast by using Users set
            // Here we assume Customer==User in domain, but if not adapt your interface
            return await _db.Set<User>().AnyAsync((Expression<Func<User, bool>>)(object)predicate, cancellationToken);
        }

        public async Task<PaginatedList<UserModel>> GetForManagementAsync(Query<User> query, CancellationToken cancellationToken = default)
        {
            // If your models/entities are named User/Customer mismatch, adapt here.
            var baseQ = _db.Set<User>().AsNoTracking();

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
                    CreatedAtUtc = u.CreatedAtUtc
                }).ToListAsync(cancellationToken);

            return new PaginatedList<UserModel>(items, count, query.PageIndex, query.PageSize);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var u = await _db.Set<User>().FindAsync(new object[] { id }, cancellationToken);
            return u == null ? null : new User
            {
                Id = u.Id,
                // map other fields if your Customer type exists; otherwise return null. 
            } as User;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var u = await _db.Set<User>().AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
            return u == null ? null : new User { Id = u.Id } as User;
        }

        public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            // If you only have User entity, adapt and create User instead.
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
