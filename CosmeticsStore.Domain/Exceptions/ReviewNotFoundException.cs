using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class ReviewNotFoundException : NotFoundException
    {
        public override string Title => "Review not found";

        public ReviewNotFoundException(string? message = null)
            : base(message ?? "Review with the given ID was not found.") { }
    }
}
