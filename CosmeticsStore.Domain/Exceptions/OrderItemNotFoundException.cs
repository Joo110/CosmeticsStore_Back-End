using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class OrderItemNotFoundException : NotFoundException
    {
        public override string Title => "Order item not found";

        public OrderItemNotFoundException(string? message = null)
            : base(message ?? "Order item with the given ID was not found.") { }
    }
}
