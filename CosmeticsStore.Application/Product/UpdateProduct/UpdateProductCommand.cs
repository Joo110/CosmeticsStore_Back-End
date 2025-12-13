using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Product.UpdateProduct
{
    public class UpdateProductCommand : IRequest<CosmeticsStore.Application.Product.AddProduct.ProductResponse>
    {
        public Guid ProductId { get; set; }

        // partial update
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public Guid? CategoryId { get; set; }
        public bool? IsPublished { get; set; }

        // simple replace strategy for variants/media (optional)
        public List<UpdateVariantDto>? Variants { get; set; }
        public List<UpdateMediaDto>? Media { get; set; }

        public class UpdateVariantDto
        {
            public Guid? ProductVariantId { get; set; }
            public string Sku { get; set; } = null!;
            public decimal PriceAmount { get; set; }
            public string PriceCurrency { get; set; } = "EGP";
            public int Stock { get; set; }
            public bool IsActive { get; set; } = true;
        }

        public class UpdateMediaDto
        {
            public Guid? MediaId { get; set; }
            public string Url { get; set; } = null!;
            public string FileName { get; set; } = null!;
            public string ContentType { get; set; } = null!;
            public long SizeInBytes { get; set; }
            public bool IsPrimary { get; set; }
        }
    }
}
