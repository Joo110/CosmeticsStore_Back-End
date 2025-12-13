using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class OrderMessages
    {
        public const string NotFound = "Order with the given ID is not found.";
        public const string InvalidStatus = "The provided order status is invalid.";
        public const string CannotCancel = "Order cannot be canceled in its current status.";
        public const string Created = "Order created successfully.";
        public const string Updated = "Order updated successfully.";
    }

}
