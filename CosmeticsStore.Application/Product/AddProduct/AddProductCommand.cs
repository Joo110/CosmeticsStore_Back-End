using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Product.AddProduct
{
    public class AddProductCommand : IRequest<CosmeticsStore.Application.Product.AddProduct.ProductResponse>
    {
        public string Name { get; set; } = null!;
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsPublished { get; set; } = false;

        // Optional: add variants/media while creating
        public List<CreateVariantDto>? Variants { get; set; }
        public List<CreateMediaDto>? Media { get; set; }

        public class CreateVariantDto
        {
            public string Sku { get; set; } = null!;
            public decimal PriceAmount { get; set; }
            public string PriceCurrency { get; set; } = "EGP";
            public int Stock { get; set; }
            public bool IsActive { get; set; } = true;
        }

        public class CreateMediaDto
        {
            public string Url { get; set; } = null!;
            public string FileName { get; set; } = null!;
            public string ContentType { get; set; } = null!;
            public long SizeInBytes { get; set; }
            public bool IsPrimary { get; set; }
        }
    }
}
