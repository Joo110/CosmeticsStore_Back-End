using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class OrderItemMessages
    {
        public const string NotFound = "Order item with the given ID is not found.";
        public const string QuantityInvalid = "Order item quantity is invalid.";
        public const string LineTotalMismatch = "Order item line total does not match unit price * quantity.";
    }
}
