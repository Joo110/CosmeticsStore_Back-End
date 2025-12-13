using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class CouponMessages
    {
        public const string NotFound = "Coupon with the given ID or code is not found.";
        public const string Expired = "The coupon is expired.";
        public const string UsageLimitReached = "The coupon usage limit has been reached.";
        public const string Invalid = "The coupon is not valid for this order.";
        public const string Applied = "Coupon applied successfully.";
    }
}
