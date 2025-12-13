using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class SkuAlreadyExistsException : ConflictException
    {
        public override string Title => "SKU already exists";

        public SkuAlreadyExistsException(string? message = null)
            : base(message ?? "A product variant with the same SKU already exists.") { }
    }
}
