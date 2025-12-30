using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Carts.Common
{
    public class CartItemDto
    {
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
