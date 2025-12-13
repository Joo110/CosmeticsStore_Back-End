using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class InventoryTransactionNotFoundException : NotFoundException
    {
        public override string Title => "Inventory transaction not found";

        public InventoryTransactionNotFoundException(string? message = null)
            : base(message ?? "Inventory transaction with the given ID was not found.") { }
    }
}
