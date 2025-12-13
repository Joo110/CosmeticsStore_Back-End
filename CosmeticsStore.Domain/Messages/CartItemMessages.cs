using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class CartItemMessages
    {
        public const string NotFound = "Cart item with the given ID is not found.";
        public const string QuantityInvalid = "Quantity must be at least 1.";
        public const string Added = "Item added to cart.";
        public const string Updated = "Cart item updated.";
        public const string Removed = "Cart item removed.";
    }
}
