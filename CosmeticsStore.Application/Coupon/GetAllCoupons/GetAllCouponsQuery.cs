using CosmeticsStore.Application.Coupon.AddCoupon;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Coupon.GetAllCoupons
{
    public class GetAllCouponsQuery : IRequest<List<CouponResponse>> { }
}
