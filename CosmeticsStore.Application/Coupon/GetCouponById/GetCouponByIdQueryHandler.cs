using CosmeticsStore.Application.Coupon.AddCoupon;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Coupon.GetCouponById
{
    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, CouponResponse>
    {
        private readonly ICouponRepository _couponRepository;

        public GetCouponByIdQueryHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<CouponResponse> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByIdAsync(request.CouponId, cancellationToken);
            if (coupon == null) throw new CouponNotFoundException("Coupon not found.");

            return new CouponResponse
            {
                CouponId = coupon.Id,
                Code = coupon.Code,
                DiscountAmount = coupon.DiscountPercentage,
                ValidFrom = coupon.ValidFromUtc,
                ValidTo = coupon.ValidUntilUtc,
                IsActive = coupon.IsActive,
                CreatedAtUtc = coupon.CreatedAtUtc,
                ModifiedAtUtc = coupon.ModifiedAtUtc
            };
        }
    }
}
