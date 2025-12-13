using CosmeticsStore.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Infrastructure.Services.Date
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDateTimeUtc()
        {
            return DateTime.UtcNow;
        }

        public DateOnly GetCurrentDateUtc()
        {
            return DateOnly.FromDateTime(DateTime.UtcNow);
        }
    }
}
