using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class Coupon : EntityBase, IAuditableEntity
    {
        public string Code { get; set; } = null!;
        public decimal DiscountPercentage { get; set; } // 0..100
        public decimal? MaxDiscountAmount { get; set; } // optional
        public DateTime ValidFromUtc { get; set; }
        public DateTime ValidUntilUtc { get; set; }
        public int UsageLimit { get; set; } = int.MaxValue;
        public int TimesUsed { get; set; }
        public bool IsActive { get; set; } = true;

        // Audit
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
