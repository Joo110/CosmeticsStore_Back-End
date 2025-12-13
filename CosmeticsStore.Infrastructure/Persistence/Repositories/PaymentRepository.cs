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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _db;
        public PaymentRepository(AppDbContext db) => _db = db;

        public async Task<bool> ExistsAsync(Expression<Func<Payment, bool>> predicate, CancellationToken cancellationToken = default)
            => await _db.Set<Payment>().AnyAsync(predicate, cancellationToken);

        public async Task<PaginatedList<PaymentModel>> GetForManagementAsync(Query<Payment> query, CancellationToken cancellationToken = default)
        {
            var baseQ = _db.Set<Payment>().AsNoTracking();

            var count = await baseQ.CountAsync(cancellationToken);

            var items = await baseQ
                .OrderByDescending(p => p.CreatedOnUtc)
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(p => new PaymentModel
                {
                    Id = p.Id,
                    OrderId = p.OrderId,
                    Amount = p.Amount,
                    Currency = p.Currency,
                    Status = p.Status,
                    Provider = p.Provider,
                    TransactionId = p.TransactionId,
                    CreatedOnUtc = p.CreatedOnUtc
                }).ToListAsync(cancellationToken);

            return new PaginatedList<PaymentModel>(items, count, query.PageIndex, query.PageSize);
        }

        public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _db.Set<Payment>().FindAsync(new object[] { id }, cancellationToken);

        public async Task<IEnumerable<Payment>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
            => await _db.Set<Payment>().Where(p => p.OrderId == orderId).ToListAsync(cancellationToken);

        public async Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            _db.Set<Payment>().Add(payment);
            await _db.SaveChangesAsync(cancellationToken);
            return payment;
        }

        public async Task UpdateAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            _db.Set<Payment>().Update(payment);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Set<Payment>().FindAsync(new object[] { id }, cancellationToken);
            if (entity == null) return;
            _db.Set<Payment>().Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
