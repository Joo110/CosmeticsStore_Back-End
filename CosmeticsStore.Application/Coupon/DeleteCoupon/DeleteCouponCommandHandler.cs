using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Coupon.DeleteCoupon
{
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, Unit>
    {
        private readonly ICouponRepository _couponRepository;

        public DeleteCouponCommandHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<Unit> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByIdAsync(request.CouponId, cancellationToken);
            if (coupon == null) throw new CouponNotFoundException("Coupon not found.");

            await _couponRepository.DeleteAsync(request.CouponId, cancellationToken);
            return Unit.Value;
        }
    }
}
