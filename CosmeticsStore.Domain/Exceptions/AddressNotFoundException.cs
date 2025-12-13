using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class AddressNotFoundException : NotFoundException
    {
        public override string Title => "Address not found";

        public AddressNotFoundException(string? message = null)
            : base(message ?? "Address with the given ID was not found.") { }
    }
}
