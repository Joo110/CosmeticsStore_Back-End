using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace CosmeticsStore.Application.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;

        public UpdateProductCommandHandler(
            IProductRepository productRepository,
            AppDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }

        public async Task<ProductModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdForUpdateAsync(request.ProductId, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {request.ProductId} not found");

            product.Name = request.Name;
            product.Slug = request.Slug;
            product.Description = request.Description;
            product.CategoryId = request.CategoryId;
            product.IsPublished = request.IsPublished;
            product.ModifiedAtUtc = DateTime.UtcNow;

            foreach (var variant in product.Variants.ToList())
            {
                _context.Entry(variant).State = EntityState.Detached;
            }

            foreach (var media in product.Media.ToList())
            {
                _context.Entry(media).State = EntityState.Detached;
            }

            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM [ProductVariants] WHERE [ProductId] = {0}",
                request.ProductId);

            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM [Media] WHERE [OwnerId] = {0}",
                request.ProductId);

            product.Variants.Clear();
            product.Media.Clear();

            if (request.Variants != null && request.Variants.Any())
            {
                foreach (var vDto in request.Variants)
                {
                    var newVariant = new ProductVariant
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        Sku = vDto.Sku,
                        PriceAmount = vDto.PriceAmount,
                        PriceCurrency = vDto.PriceCurrency,
                        StockQuantity = vDto.Stock,
                        IsActive = vDto.IsActive,
                        CreatedAtUtc = DateTime.UtcNow,
                        ModifiedAtUtc = DateTime.UtcNow
                    };

                    // ⭐ أضف للـ context مباشرة
                    await _context.Set<ProductVariant>().AddAsync(newVariant, cancellationToken);
                }
            }

            // أضف الـ Media الجديدة
            if (request.Media != null && request.Media.Any())
            {
                foreach (var mDto in request.Media)
                {
                    if (mDto.MediaId.HasValue && mDto.MediaId != Guid.Empty)
                    {
                        var newMedia = new CosmeticsStore.Domain.Entities.Media
                        {
                            Id = mDto.MediaId.Value,
                            OwnerId = product.Id,
                            Url = mDto.Url,
                            FileName = mDto.FileName,
                            ContentType = mDto.ContentType,
                            SizeInBytes = mDto.SizeInBytes,
                            IsPrimary = mDto.IsPrimary,
                            CreatedAtUtc = DateTime.UtcNow,
                            ModifiedAtUtc = DateTime.UtcNow
                        };

                        // ⭐ أضف للـ context مباشرة
                        await _context.Set<CosmeticsStore.Domain.Entities.Media>().AddAsync(newMedia, cancellationToken);
                    }
                }
            }

            // احفظ التغييرات
            await _context.SaveChangesAsync(cancellationToken);

            // ⭐ أعد تحميل الـ product بعد الحفظ عشان نجيب الـ relations الجديدة
            var updatedProduct = await _context.Set<CosmeticsStore.Domain.Entities.Product>()
                .Include(p => p.Variants)
                .Include(p => p.Media)
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            // Return updated product
            return new ProductModel
            {
                Id = updatedProduct!.Id,
                Name = updatedProduct.Name,
                Slug = updatedProduct.Slug,
                Description = updatedProduct.Description,
                CategoryId = updatedProduct.CategoryId,
                IsPublished = updatedProduct.IsPublished,
                CreatedAtUtc = updatedProduct.CreatedAtUtc,
                ModifiedAtUtc = updatedProduct.ModifiedAtUtc,

                Variants = updatedProduct.Variants.Select(v => new ProductVariantModel
                {
                    Id = v.Id,
                    Sku = v.Sku,
                    PriceAmount = v.PriceAmount,
                    PriceCurrency = v.PriceCurrency,
                    StockQuantity = v.StockQuantity,
                    IsActive = v.IsActive
                }).ToList(),

                Media = updatedProduct.Media.Select(m => new MediaModel
                {
                    Id = m.Id,
                    OwnerId = m.OwnerId,
                    Url = m.Url,
                    FileName = m.FileName,
                    ContentType = m.ContentType,
                    SizeInBytes = m.SizeInBytes,
                    IsPrimary = m.IsPrimary,
                    CreatedAtUtc = m.CreatedAtUtc
                }).ToList()
            };
        }
    }
}