using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Coupon.UpdateCoupon
{
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, Unit>
    {
        private readonly ICouponRepository _couponRepository;

        public UpdateCouponCommandHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<Unit> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByIdAsync(request.CouponId, cancellationToken);
            if (coupon == null) throw new CouponNotFoundException("Coupon not found.");

            coupon.Code = request.Code;
            coupon.DiscountPercentage = request.DiscountAmount;
            coupon.ValidFromUtc = request.ValidFrom;
            coupon.ValidUntilUtc = request.ValidTo;
            coupon.IsActive = request.IsActive;
            coupon.ModifiedAtUtc = DateTime.UtcNow;

            await _couponRepository.UpdateAsync(coupon, cancellationToken);
            return Unit.Value;
        }
    }
}
