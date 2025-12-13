using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class CartNotFoundException : NotFoundException
    {
        public override string Title => "Cart not found";

        public CartNotFoundException(string? message = null)
            : base(message ?? "Cart with the given ID was not found.") { }
    }
}
