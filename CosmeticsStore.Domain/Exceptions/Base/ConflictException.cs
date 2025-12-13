using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions.Base;

public class ConflictException : CustomException
{
    public ConflictException(string message)
        : base(message, "Conflict")
    {
    }
}
