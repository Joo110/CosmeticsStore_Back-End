using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class CartMessages
    {
        public const string NotFound = "Cart with the given ID is not found.";
        public const string Empty = "Cart is empty.";
        public const string ItemNotFound = "Cart item not found in the cart.";
        public const string Updated = "Cart updated successfully.";
        public const string Cleared = "Cart cleared successfully.";
    }
}
