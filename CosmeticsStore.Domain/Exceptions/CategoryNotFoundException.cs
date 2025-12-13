using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public override string Title => "Category not found";

        public CategoryNotFoundException(string? message = null)
            : base(message ?? "Category with the given ID was not found.") { }
    }
}
