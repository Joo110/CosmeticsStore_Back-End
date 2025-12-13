using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Interfaces.Services
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentDateTimeUtc();
        DateOnly GetCurrentDateUtc();
    }
}
