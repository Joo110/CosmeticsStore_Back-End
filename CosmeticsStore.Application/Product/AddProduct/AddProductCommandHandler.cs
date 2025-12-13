using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Product.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, CosmeticsStore.Application.Product.AddProduct.ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CosmeticsStore.Application.Product.AddProduct.ProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new CosmeticsStore.Domain.Entities.Product
            {
                Name = request.Name,
                Slug = request.Slug,
                Description = request.Description,
                CategoryId = request.CategoryId,
                IsPublished = request.IsPublished,
                CreatedAtUtc = DateTime.UtcNow
            };

            // map variants if provided (adjust fields to match your ProductVariant entity)
            if (request.Variants != null && request.Variants.Any())
            {
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

            // map media if provided (adjust to your Media entity)
            if (request.Media != null && request.Media.Any())
            {
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

            var created = await _productRepository.CreateAsync(product, cancellationToken);

            return new CosmeticsStore.Application.Product.AddProduct.ProductResponse
            {
                ProductId = created.Id,
                Name = created.Name,
                Slug = created.Slug,
                Description = created.Description,
                CategoryId = created.CategoryId,
                IsPublished = created.IsPublished,
                CreatedAtUtc = created.CreatedAtUtc,
                ModifiedAtUtc = created.ModifiedAtUtc,
                Variants = created.Variants?.Select(v => new CosmeticsStore.Application.Product.AddProduct.ProductResponse.ProductVariantResponse
                {
                    ProductVariantId = v.Id,
                    Sku = v.Sku,
                    PriceAmount = v.PriceAmount,
                    PriceCurrency = v.PriceCurrency,
                    Stock = v.StockQuantity,
                    IsActive = v.IsActive
                }).ToList(),
                Media = created.Media?.Select(mm => new CosmeticsStore.Application.Product.AddProduct.ProductResponse.MediaResponse
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