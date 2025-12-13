namespace CosmeticsStore.Dtos.Product
{
    // POST: create product (optional variants & media)
    public class AddProductRequest
    {
        public string Name { get; set; } = null!;
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsPublished { get; set; } = false;

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

    // PUT: partial update
    public class UpdateProductRequest
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public Guid? CategoryId { get; set; }
        public bool? IsPublished { get; set; }

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

    // Response DTO
    public class ProductResponseDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsPublished { get; set; }

        public List<ProductVariantResponseDto>? Variants { get; set; }
        public List<MediaResponseDto>? Media { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }

        public class ProductVariantResponseDto
        {
            public Guid ProductVariantId { get; set; }
            public string Sku { get; set; } = null!;
            public decimal PriceAmount { get; set; }
            public string PriceCurrency { get; set; } = "EGP";
            public int Stock { get; set; }
            public bool IsActive { get; set; } = true;
        }

        public class MediaResponseDto
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
