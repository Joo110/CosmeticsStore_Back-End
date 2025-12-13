using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Product.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedList<CosmeticsStore.Application.Product.AddProduct.ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PaginatedList<CosmeticsStore.Application.Product.AddProduct.ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var query = new Query<Domain.Entities.Product>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm
            };

            // NOTE: Query<T> in your Domain.Models currently doesn't have Filter expression.
            // Prefer implementing filtering in repository using Query.SearchTerm / extra params.
            var paged = await _productRepository.GetForManagementAsync(query, cancellationToken);

            var items = paged.Items.Select(m => new CosmeticsStore.Application.Product.AddProduct.ProductResponse
            {
                ProductId = m.Id,
                Name = m.Name,
                Slug = m.Slug,
                Description = m.Description,
                CategoryId = m.CategoryId,
                IsPublished = m.IsPublished,
                CreatedAtUtc = m.CreatedAtUtc,
                ModifiedAtUtc = m.ModifiedAtUtc,
                Variants = m.Variants?.Select(v => new CosmeticsStore.Application.Product.AddProduct.ProductResponse.ProductVariantResponse
                {
                    ProductVariantId = v.Id,
                    Sku = v.Sku,
                    PriceAmount = v.PriceAmount,
                    PriceCurrency = v.PriceCurrency,
                    Stock = v.StockQuantity,
                    IsActive = v.IsActive
                }).ToList(),
                Media = m.Media?.Select(md => new CosmeticsStore.Application.Product.AddProduct.ProductResponse.MediaResponse
                {
                    MediaId = md.Id,
                    Url = md.Url,
                    FileName = md.FileName,
                    ContentType = md.ContentType,
                    SizeInBytes = md.SizeInBytes,
                    IsPrimary = md.IsPrimary,
                    CreatedAtUtc = md.CreatedAtUtc
                }).ToList()
            }).ToList();

            var result = new PaginatedList<CosmeticsStore.Application.Product.AddProduct.ProductResponse>(
                items,
                paged.TotalCount,
                paged.PageIndex,
                request.PageSize
            );

            return result;
        }
    }
}