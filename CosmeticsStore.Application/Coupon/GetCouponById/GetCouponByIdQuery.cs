using CosmeticsStore.Application.Coupon.AddCoupon;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Coupon.GetCouponById
{
    public class GetCouponByIdQuery : IRequest<CouponResponse>
    {
        public Guid CouponId { get; }

        public GetCouponByIdQuery(Guid couponId) => CouponId = couponId;
    }
}
