using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions.Base
{
    public class ForbiddenException : CustomException
    {
        public ForbiddenException(string message)
            : base(message, "Forbidden")
        {
        }
    }
}
