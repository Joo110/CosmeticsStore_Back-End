using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class ProductVariantNotFoundException : NotFoundException
    {
        public override string Title => "Product variant not found";

        public ProductVariantNotFoundException(string? message = null)
            : base(message ?? "Product variant with the given ID was not found.") { }
    }
}
