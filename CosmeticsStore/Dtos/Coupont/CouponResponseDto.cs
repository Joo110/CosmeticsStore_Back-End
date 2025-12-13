namespace CosmeticsStore.Dtos.Coupont
{
    public class CouponResponseDto
    {
        public Guid CouponId { get; set; }
        public string Code { get; set; } = null!;
        public decimal DiscountAmount { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
