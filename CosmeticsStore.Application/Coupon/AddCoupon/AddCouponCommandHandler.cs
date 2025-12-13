using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Coupon.AddCoupon
{
    public class AddCouponCommandHandler : IRequestHandler<AddCouponCommand, Guid>
    {
        private readonly ICouponRepository _couponRepository;

        public AddCouponCommandHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<Guid> Handle(AddCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = new Domain.Entities.Coupon
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                DiscountPercentage = request.DiscountAmount,
                ValidFromUtc = request.ValidFrom,
                ValidUntilUtc = request.ValidTo,
                IsActive = request.IsActive,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _couponRepository.CreateAsync(coupon, cancellationToken);
            return coupon.Id;
        }
    }
}
