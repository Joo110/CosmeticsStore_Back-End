using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions.Base
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message)
            : base(message, "Unauthorized")
        {
        }
    }
}
