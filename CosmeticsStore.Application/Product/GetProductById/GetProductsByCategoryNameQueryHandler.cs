using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Product.GetProductsByCategoryName
{
    public class GetProductsByCategoryNameQueryHandler
        : IRequestHandler<
            GetProductsByCategoryNameQuery,
            IEnumerable<CosmeticsStore.Application.Product.AddProduct.ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByCategoryNameQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<CosmeticsStore.Application.Product.AddProduct.ProductResponse>> Handle(
            GetProductsByCategoryNameQuery request,
            CancellationToken cancellationToken)
        {
            var products = await _productRepository
                .GetByCategoryNameAsync(request.CategoryName, cancellationToken);

            return products.Select(product => new CosmeticsStore.Application.Product.AddProduct.ProductResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                CategoryId = product.CategoryId,
                IsPublished = product.IsPublished,
                CreatedAtUtc = product.CreatedAtUtc,
                ModifiedAtUtc = product.ModifiedAtUtc,

                Variants = product.Variants?.Select(v =>
                    new CosmeticsStore.Application.Product.AddProduct.ProductResponse.ProductVariantResponse
                    {
                        ProductVariantId = v.Id,
                        Sku = v.Sku,
                        PriceAmount = v.PriceAmount,
                        PriceCurrency = v.PriceCurrency,
                        Stock = v.StockQuantity,
                        IsActive = v.IsActive
                    }).ToList(),

                Media = product.Media?.Select(mm =>
                    new CosmeticsStore.Application.Product.AddProduct.ProductResponse.MediaResponse
                    {
                        MediaId = mm.Id,
                        Url = mm.Url,
                        FileName = mm.FileName,
                        ContentType = mm.ContentType,
                        SizeInBytes = mm.SizeInBytes,
                        IsPrimary = mm.IsPrimary,
                        CreatedAtUtc = mm.CreatedAtUtc
                    }).ToList()
            });
        }
    }
}