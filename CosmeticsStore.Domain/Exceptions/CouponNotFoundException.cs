using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class CouponNotFoundException : NotFoundException
    {
        public override string Title => "Coupon not found";

        public CouponNotFoundException(string? message = null)
            : base(message ?? "Coupon with the given code or ID was not found.") { }
    }
}
