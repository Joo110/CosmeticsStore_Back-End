using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Constants
{
    public static class ProductVariantConstants
    {
        public const int SkuMaxLength = 64;
        public const int TitleMaxLength = 200;
        public const decimal MinPriceAmount = 0m;
        public const int MaxStockQuantity = 1000000;
    }
}
