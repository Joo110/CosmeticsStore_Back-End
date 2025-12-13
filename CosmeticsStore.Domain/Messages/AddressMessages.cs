using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class AddressMessages
    {
        public const string NotFound = "Address with the given ID is not found.";
        public const string NotFoundForUser = "Address is not found for the specified user.";
        public const string Invalid = "The provided address is invalid.";
        public const string Created = "Address created successfully.";
        public const string Updated = "Address updated successfully.";
        public const string Deleted = "Address deleted successfully.";
    }
}
