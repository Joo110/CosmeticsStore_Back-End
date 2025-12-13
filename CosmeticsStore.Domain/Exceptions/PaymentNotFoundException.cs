using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class PaymentNotFoundException : NotFoundException
    {
        public override string Title => "Payment not found";

        public PaymentNotFoundException(string? message = null)
            : base(message ?? "Payment with the given ID was not found.") { }
    }
}
