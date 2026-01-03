using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsStore.Application.Product.UpdateProduct
{
    public class UpdateProductCommandHandler
        : IRequestHandler<UpdateProductCommand, ProductModel>
    {
        private readonly AppDbContext _context;

        public UpdateProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductModel> Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(p => p.Variants)
                .Include(p => p.Media)
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {request.ProductId} not found");

            // 🟢 Update main fields
            product.Name = request.Name;
            product.Slug = request.Slug;
            product.Description = request.Description;
            product.CategoryId = request.CategoryId;
            product.IsPublished = request.IsPublished;
            product.ModifiedAtUtc = DateTime.UtcNow;

            // =========================
            // 🟢 Variants (Update / Add only)
            // =========================
            if (request.Variants != null)
            {
                foreach (var v in request.Variants)
                {
                    var existingVariant = product.Variants
                        .FirstOrDefault(x => x.Id == v.ProductVariantId);

                    if (existingVariant != null)
                    {
                        // Update existing
                        existingVariant.Sku = v.Sku;
                        existingVariant.PriceAmount = v.PriceAmount;
                        existingVariant.PriceCurrency = v.PriceCurrency;
                        existingVariant.StockQuantity = v.Stock;
                        existingVariant.IsActive = v.IsActive;
                        existingVariant.ModifiedAtUtc = DateTime.UtcNow;
                    }
                    else
                    {
                        // Add new
                        product.Variants.Add(new ProductVariant
                        {
                            Id = Guid.NewGuid(),
                            ProductId = product.Id,
                            Sku = v.Sku,
                            PriceAmount = v.PriceAmount,
                            PriceCurrency = v.PriceCurrency,
                            StockQuantity = v.Stock,
                            IsActive = v.IsActive,
                            CreatedAtUtc = DateTime.UtcNow,
                            ModifiedAtUtc = DateTime.UtcNow
                        });
                    }
                }
            }

            // =========================
            // 🟢 Media (Update / Add only)
            // =========================
            if (request.Media != null)
            {
                foreach (var m in request.Media)
                {
                    var existingMedia = product.Media
                        .FirstOrDefault(x => x.Id == m.MediaId);

                    if (existingMedia != null)
                    {
                        existingMedia.Url = m.Url;
                        existingMedia.FileName = m.FileName;
                        existingMedia.ContentType = m.ContentType;
                        existingMedia.SizeInBytes = m.SizeInBytes;
                        existingMedia.IsPrimary = m.IsPrimary;
                        existingMedia.ModifiedAtUtc = DateTime.UtcNow;
                    }
                    else
                    {
                        product.Media.Add(new CosmeticsStore.Domain.Entities.Media
                        {
                            Id = Guid.NewGuid(),
                            OwnerId = product.Id,
                            Url = m.Url,
                            FileName = m.FileName,
                            ContentType = m.ContentType,
                            SizeInBytes = m.SizeInBytes,
                            IsPrimary = m.IsPrimary,
                            CreatedAtUtc = DateTime.UtcNow,
                            ModifiedAtUtc = DateTime.UtcNow
                        });
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            // =========================
            // 🟢 Response (زي ما هو)
            // =========================
            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                CategoryId = product.CategoryId,
                IsPublished = product.IsPublished,
                CreatedAtUtc = product.CreatedAtUtc,
                ModifiedAtUtc = product.ModifiedAtUtc,

                Variants = product.Variants.Select(v => new ProductVariantModel
                {
                    Id = v.Id,
                    Sku = v.Sku,
                    PriceAmount = v.PriceAmount,
                    PriceCurrency = v.PriceCurrency,
                    StockQuantity = v.StockQuantity,
                    IsActive = v.IsActive
                }).ToList(),

                Media = product.Media.Select(m => new MediaModel
                {
                    Id = m.Id,
                    OwnerId = product.Id,
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
