using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Constants
{
    public static class CouponConstants
    {
        public const int CodeMaxLength = 50;
        public const decimal MinDiscountPercentage = 0m;
        public const decimal MaxDiscountPercentage = 100m;
        public const decimal MaxDiscountAmount = 1_000_000m;
        public const int MaxUsageLimit = 1_000_000;
    }
}
