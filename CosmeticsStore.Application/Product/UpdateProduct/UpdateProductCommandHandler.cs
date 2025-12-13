using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, CosmeticsStore.Application.Product.AddProduct.ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CosmeticsStore.Application.Product.AddProduct.ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product == null)
                throw new CosmeticsStore.Domain.Exceptions.ProductNotFoundException($"Product with id {request.ProductId} not found.");

            if (request.Name != null) product.Name = request.Name;
            if (request.Slug != null) product.Slug = request.Slug;
            if (request.Description != null) product.Description = request.Description;
            if (request.CategoryId.HasValue) product.CategoryId = request.CategoryId.Value;
            if (request.IsPublished.HasValue) product.IsPublished = request.IsPublished.Value;

            // Replace variants if provided (simple approach)
            if (request.Variants != null)
            {
                product.Variants.Clear();
                foreach (var v in request.Variants)
                {
                    product.Variants.Add(new ProductVariant
                    {
                        Sku = v.Sku,
                        PriceAmount = v.PriceAmount,
                        PriceCurrency = v.PriceCurrency,
                        StockQuantity = v.Stock,
                        IsActive = v.IsActive
                    });
                }
            }

            // Replace media if provided (simple approach)
            if (request.Media != null)
            {
                product.Media.Clear();
                foreach (var m in request.Media)
                {
                    product.Media.Add(new CosmeticsStore.Domain.Entities.Media
                    {
                        Url = m.Url,
                        FileName = m.FileName,
                        ContentType = m.ContentType,
                        SizeInBytes = m.SizeInBytes,
                        IsPrimary = m.IsPrimary,
                        CreatedAtUtc = DateTime.UtcNow
                    });
                }
            }

            product.ModifiedAtUtc = DateTime.UtcNow;

            await _productRepository.UpdateAsync(product, cancellationToken);

            return new CosmeticsStore.Application.Product.AddProduct.ProductResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                CategoryId = product.CategoryId,
                IsPublished = product.IsPublished,
                CreatedAtUtc = product.CreatedAtUtc,
                ModifiedAtUtc = product.ModifiedAtUtc,
                Variants = product.Variants?.Select(v => new CosmeticsStore.Application.Product.AddProduct.ProductResponse.ProductVariantResponse
                {
                    ProductVariantId = v.Id,
                    Sku = v.Sku,
                    PriceAmount = v.PriceAmount,
                    PriceCurrency = v.PriceCurrency,
                    Stock = v.StockQuantity,
                    IsActive = v.IsActive
                }).ToList(),
                Media = product.Media?.Select(mm => new CosmeticsStore.Application.Product.AddProduct.ProductResponse.MediaResponse
                {
                    MediaId = mm.Id,
                    Url = mm.Url,
                    FileName = mm.FileName,
                    ContentType = mm.ContentType,
                    SizeInBytes = mm.SizeInBytes,
                    IsPrimary = mm.IsPrimary,
                    CreatedAtUtc = mm.CreatedAtUtc
                }).ToList()
            };
        }
    }
}
