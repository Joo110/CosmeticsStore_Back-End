using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class CategoryMessages
    {
        public const string NotFound = "Category with the given ID is not found.";
        public const string NameAlreadyExists = "A category with the same name already exists.";
        public const string Invalid = "The provided category data is invalid.";
        public const string Created = "Category created successfully.";
        public const string Updated = "Category updated successfully.";
        public const string Deleted = "Category deleted successfully.";
    }
}
