using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class CategoryAlreadyExistsException : ConflictException
    {
        public override string Title => "Category already exists";

        public CategoryAlreadyExistsException(string? message = null)
            : base(message ?? "A category with the same name already exists.") { }
    }
}
