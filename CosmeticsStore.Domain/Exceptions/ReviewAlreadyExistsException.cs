using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class ReviewAlreadyExistsException : ConflictException
    {
        public override string Title => "Review already exists";

        public ReviewAlreadyExistsException(string? message = null)
            : base(message ?? "User already submitted a review for this product.") { }
    }
}
