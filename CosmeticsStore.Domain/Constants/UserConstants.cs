using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Constants
{
    public static class UserConstants
    {
        public const int MinFullNameLength = 3;
        public const int MaxFullNameLength = 80;
        public const int MinPasswordLength = 6;
        public const int MaxPasswordLength = 128;
        public const int MaxEmailLength = 256;
        public const int MaxPhoneLength = 20;

        public const string DefaultRole = "User";
        public const string AdminRole = "Admin";
    }
}
