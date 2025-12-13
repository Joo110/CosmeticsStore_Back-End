using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class MediaNotFoundException : NotFoundException
    {
        public override string Title => "Media not found";

        public MediaNotFoundException(string? message = null)
            : base(message ?? "Media with the given ID was not found.") { }
    }
}
