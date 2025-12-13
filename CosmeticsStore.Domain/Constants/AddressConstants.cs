using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Constants
{
    public static class AddressConstants
    {
        public const int StreetMinLength = 3;
        public const int StreetMaxLength = 200;
        public const int CityMaxLength = 100;
        public const int StateMaxLength = 100;
        public const int CountryMaxLength = 100;
        public const int PostalCodeMaxLength = 20;
    }
}
