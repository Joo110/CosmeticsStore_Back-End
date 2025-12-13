using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class CouponExpiredException : ConflictException
    {
        public override string Title => "Coupon expired";

        public CouponExpiredException(string? message = null)
            : base(message ?? "The coupon has expired and cannot be used.") { }
    }
}
