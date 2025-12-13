using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Coupon.DeleteCoupon
{
    public class DeleteCouponCommand : IRequest<Unit>
    {
        public Guid CouponId { get; }

        public DeleteCouponCommand(Guid couponId)
        {
            CouponId = couponId;
        }
    }
}
