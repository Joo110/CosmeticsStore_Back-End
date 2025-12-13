using CosmeticsStore.Application.Coupon.AddCoupon;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Domain.Entities;
using MediatR;

namespace CosmeticsStore.Application.Coupon.GetAllCoupons
{
    public class GetAllCouponsQueryHandler : IRequestHandler<GetAllCouponsQuery, List<CouponResponse>>
    {
        private readonly ICouponRepository _couponRepository;

        public GetAllCouponsQueryHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<List<CouponResponse>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
        {
            var query = new Query<CosmeticsStore.Domain.Entities.Coupon>();

            var paginatedCoupons = await _couponRepository.GetForManagementAsync(query, cancellationToken);

            return paginatedCoupons.Items.Select(c => new CouponResponse
            {
                CouponId = c.Id,
                Code = c.Code,
                DiscountAmount = c.DiscountPercentage,
                ValidFrom = c.ValidFromUtc,
                ValidTo = c.ValidUntilUtc,
                IsActive = c.IsActive,
                CreatedAtUtc = c.CreatedAtUtc,
                ModifiedAtUtc = c.CreatedAtUtc
            }).ToList();
        }
    }
}
