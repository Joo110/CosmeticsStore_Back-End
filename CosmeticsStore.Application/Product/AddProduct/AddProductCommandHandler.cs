using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;

namespace CosmeticsStore.Application.Product.AddProduct
{
    public class AddProductCommandHandler
        : IRequestHandler<AddProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(
     AddProductCommand request,
     CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product
            {
                Name = request.Name,
                Slug = request.Slug,
                Description = request.Description,
                CategoryId = request.CategoryId,
                IsPublished = request.IsPublished,
                CreatedAtUtc = DateTime.UtcNow
            };

            // ⭐ VARIANTS
            if (request.Variants != null && request.Variants.Any())
            {
                foreach (var v in request.Variants)
                {
                    product.Variants.Add(new ProductVariant
                    {
                        Product = product,
                        Sku = v.Sku,
                        PriceAmount = v.PriceAmount,
                        PriceCurrency = v.PriceCurrency,
                        StockQuantity = v.Stock,
                        IsActive = v.IsActive
                    });
                }
            }

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

            return new ProductResponse
            {
                ProductId = created.Id,
                Name = created.Name,
                Slug = created.Slug,
                Description = created.Description,
                CategoryId = created.CategoryId,
                IsPublished = created.IsPublished,
                CreatedAtUtc = created.CreatedAtUtc,
                ModifiedAtUtc = created.ModifiedAtUtc,

                Variants = created.Variants.Select(v => new ProductResponse.ProductVariantResponse
                {
                    ProductVariantId = v.Id,
                    Sku = v.Sku,
                    PriceAmount = v.PriceAmount,
                    PriceCurrency = v.PriceCurrency,
                    Stock = v.StockQuantity,
                    IsActive = v.IsActive
                }).ToList(),

                Media = created.Media.Select(m => new ProductResponse.MediaResponse
                {
                    MediaId = m.Id,
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
