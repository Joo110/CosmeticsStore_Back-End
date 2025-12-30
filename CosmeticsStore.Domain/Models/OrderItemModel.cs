using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Models
{
    public class OrderItemModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductVariantId { get; set; }
        public string ProductName { get; set; }
        public decimal ItemTotalPrice { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPriceAmount { get; set; }
        public string UnitPriceCurrency { get; set; }
        public string? Title { get; set; }
        public decimal LineTotal { get; set; }
    }
}
