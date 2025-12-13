using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Constants
{
    public static class OrderConstants
    {
        // Status values (use constants to avoid magic strings across the codebase)
        public const string StatusPending = "Pending";
        public const string StatusPaid = "Paid";
        public const string StatusProcessing = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";

        public const int MaxItemsPerOrder = 500;
    }
}
