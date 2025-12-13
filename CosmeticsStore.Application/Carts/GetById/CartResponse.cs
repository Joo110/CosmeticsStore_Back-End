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
        public IReadOnlyList<CartItemResponse> Items { get; init; }
        public decimal TotalPrice { get; init; }
    }

    public class CartItemResponse
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public decimal SubTotal { get; init; }
    }
}
