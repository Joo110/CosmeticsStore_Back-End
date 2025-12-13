using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class CartItemNotFoundException : NotFoundException
    {
        public override string Title => "Cart item not found";

        public CartItemNotFoundException(string? message = null)
            : base(message ?? "Cart item with the given ID was not found.") { }
    }
}
