using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Carts.GetById
{
    public class CartResponse
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public IReadOnlyList<CartItemResponse> Items { get; init; } = Array.Empty<CartItemResponse>();
        public decimal TotalAmount { get; init; }
        public string Currency { get; init; } = "EGP";
        public DateTime CreatedAtUtc { get; init; }
        public DateTime? ModifiedAtUtc { get; init; }
    }

    public class CartItemResponse
    {
        public Guid Id { get; init; }
        public Guid ProductVariantId { get; init; }
        public string? Title { get; init; }
        public decimal UnitPriceAmount { get; init; } 
        public string UnitPriceCurrency { get; init; } = "EGP";
        public int Quantity { get; init; }
        public decimal LineTotal { get; init; }
    }
}