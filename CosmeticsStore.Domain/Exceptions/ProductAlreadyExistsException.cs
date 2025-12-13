using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class ProductAlreadyExistsException : ConflictException
    {
        public override string Title => "Product already exists";

        public ProductAlreadyExistsException(string? message = null)
            : base(message ?? "A product with the same name already exists.") { }
    }
}
