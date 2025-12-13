using MediatR;
using System;

namespace CosmeticsStore.Application.Coupon.UpdateCoupon
{
    public class UpdateCouponCommand : IRequest<Unit>
    {
        public Guid CouponId { get; set; }            // <- make it settable
        public string Code { get; set; } = null!;
        public decimal DiscountAmount { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsActive { get; set; }

        public UpdateCouponCommand() { } // parameterless for AutoMapper

        public UpdateCouponCommand(Guid couponId, string code, decimal discountAmount, DateTime validFrom, DateTime validTo, bool isActive)
        {
            CouponId = couponId;
            Code = code;
            DiscountAmount = discountAmount;
            ValidFrom = validFrom;
            ValidTo = validTo;
            IsActive = isActive;
        }
    }
}
