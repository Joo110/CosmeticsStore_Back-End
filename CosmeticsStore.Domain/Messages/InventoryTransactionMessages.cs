using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class InventoryTransactionMessages
    {
        public const string NotFound = "Inventory transaction with the given ID is not found.";
        public const string InvalidQuantity = "Quantity changed must be non-zero.";
        public const string RelatedOrderNotFound = "Related order not found for this transaction.";
        public const string Recorded = "Inventory transaction recorded successfully.";
    }
}
