using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using Google;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Infrastructure.Persistence.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly AppDbContext _db;

        public ProductVariantRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ProductVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.ProductVariant
                .FirstOrDefaultAsync(pv => pv.Id == id, cancellationToken);
        }

        public async Task<string?> GetProductNameByVariantIdAsync(Guid variantId, CancellationToken cancellationToken = default)
        {
            return await _db.ProductVariant
                .Where(pv => pv.Id == variantId)
                .Select(pv => pv.Product.Name)
                .FirstOrDefaultAsync(cancellationToken);
        }

    }
}
