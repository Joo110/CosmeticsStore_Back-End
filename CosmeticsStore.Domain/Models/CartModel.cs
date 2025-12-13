using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Models
{
    public class CartModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<CartItemModel> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string TotalCurrency { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
