using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class ProductMessages
    {
        public const string NotFound = "Product with the given ID is not found.";
        public const string SlugAlreadyExists = "A product with the same slug already exists.";
        public const string HasVariants = "Product has variants and cannot be removed directly.";
        public const string NotPublished = "Product is not published.";
        public const string Created = "Product created successfully.";
        public const string Updated = "Product updated successfully.";
        public const string Deleted = "Product deleted successfully.";
    }
}
