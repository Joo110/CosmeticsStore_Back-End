using CosmeticsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Interfaces.Persistence.Repositories
{
    public interface IProductVariantRepository
    {
        Task<string?> GetProductNameByVariantIdAsync(Guid variantId, CancellationToken cancellationToken = default); // ✅ جديد

        Task<ProductVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
