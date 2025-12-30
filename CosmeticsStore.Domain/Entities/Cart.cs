using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Entities
{
    public class Cart : EntityBase
    {
        public Guid UserId { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public void AddItem(Guid productVariantId, int quantity, decimal priceAmount, string priceCurrency, string? title)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductVariantId == productVariantId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                ModifiedAtUtc = DateTime.UtcNow;
            }
            else
            {
                var newItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = this.Id,
                    ProductVariantId = productVariantId,
                    Quantity = quantity,
                    UnitPriceAmount = priceAmount,
                    UnitPriceCurrency = priceCurrency,
                    Title = title,
                    CreatedAtUtc = DateTime.UtcNow
                };

                Items.Add(newItem);
            }

            ModifiedAtUtc = DateTime.UtcNow;
        }


        public void UpdateItemQuantity(Guid itemId, int newQuantity)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null) throw new InvalidOperationException("Item not found");
            if (newQuantity <= 0)
            {
                Items.Remove(item);
            }
            else
            {
                item.Quantity = newQuantity;
            }

            ModifiedAtUtc = DateTime.UtcNow;
        }

        public void RemoveItem(Guid itemId)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                Items.Remove(item);
                ModifiedAtUtc = DateTime.UtcNow;
            }
        }

        public decimal TotalAmount => Items.Sum(i => i.UnitPriceAmount * i.Quantity);
    }
}
