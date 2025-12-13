using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public override string Title => "Product not found";

        public ProductNotFoundException(string? message = null)
            : base(message ?? "Product with the given ID was not found.") { }
    }
}
