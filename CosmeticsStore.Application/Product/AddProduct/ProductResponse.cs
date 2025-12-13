using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Product.AddProduct
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsPublished { get; set; }

        public List<ProductVariantResponse>? Variants { get; set; }
        public List<MediaResponse>? Media { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }

        public class ProductVariantResponse
        {
            public Guid ProductVariantId { get; set; }
            public string Sku { get; set; } = null!;
            public decimal PriceAmount { get; set; }
            public string PriceCurrency { get; set; } = "EGP";
            public int Stock { get; set; }
            public bool IsActive { get; set; } = true;
        }

        public class MediaResponse
        {
            public Guid MediaId { get; set; }
            public string Url { get; set; } = null!;
            public string FileName { get; set; } = null!;
            public string ContentType { get; set; } = null!;
            public long SizeInBytes { get; set; }
            public bool IsPrimary { get; set; }
            public DateTime CreatedAtUtc { get; set; }
        }
    }
}
