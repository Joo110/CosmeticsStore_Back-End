using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class CouponUsageLimitExceededException : ConflictException
    {
        public override string Title => "Coupon usage limit exceeded";

        public CouponUsageLimitExceededException(string? message = null)
            : base(message ?? "The coupon has reached its usage limit.") { }
    }
}
