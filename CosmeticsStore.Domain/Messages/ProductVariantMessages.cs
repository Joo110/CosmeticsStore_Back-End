using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class ProductVariantMessages
    {
        public const string NotFound = "Product variant with the given ID is not found.";
        public const string SkuAlreadyExists = "A variant with the same SKU already exists.";
        public const string OutOfStock = "The requested variant is out of stock.";
        public const string Created = "Product variant created successfully.";
        public const string Updated = "Product variant updated successfully.";
        public const string Deleted = "Product variant deleted successfully.";
    }
}
