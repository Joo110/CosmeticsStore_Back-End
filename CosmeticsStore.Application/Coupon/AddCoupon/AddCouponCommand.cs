using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Coupon.AddCoupon
{
    public class AddCouponCommand : IRequest<Guid>
    {
        public string Code { get; }
        public decimal DiscountAmount { get; }
        public DateTime ValidFrom { get; }
        public DateTime ValidTo { get; }
        public bool IsActive { get; }

        public AddCouponCommand(string code, decimal discountAmount, DateTime validFrom, DateTime validTo, bool isActive)
        {
            Code = code;
            DiscountAmount = discountAmount;
            ValidFrom = validFrom;
            ValidTo = validTo;
            IsActive = isActive;
        }
    }
}
