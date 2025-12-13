using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public override string Title => "Order not found";

        public OrderNotFoundException(string? message = null)
            : base(message ?? "Order with the given ID was not found.") { }
    }
}
