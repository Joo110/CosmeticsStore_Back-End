using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Constants
{
    public static class ProductConstants
    {
        public const int NameMinLength = 2;
        public const int NameMaxLength = 200;
        public const int SlugMaxLength = 200;
        public const int DescriptionMaxLength = 2000;

        public const int MaxVariantsPerProduct = 50;
    }
}
