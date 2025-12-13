using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions.Base
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message)
            : base(message, "Bad Request")
        {
        }
    }
}
