using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsPublished { get; set; }
        public IEnumerable<ProductVariantModel> Variants { get; set; }
        public IEnumerable<MediaModel> Media { get; set; }
        public IEnumerable<ReviewModel> Reviews { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
