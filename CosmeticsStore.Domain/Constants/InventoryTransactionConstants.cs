using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Constants
{
    public static class InventoryTransactionConstants
    {
        public const int NoteMaxLength = 1000;

        // Transaction types (use these constants where you compare/check types)
        public const string TypeSale = "Sale";
        public const string TypeRestock = "Restock";
        public const string TypeAdjustment = "Adjustment";
        public const string TypeReservation = "Reservation";
        public const string TypeRelease = "Release";
    }
}
