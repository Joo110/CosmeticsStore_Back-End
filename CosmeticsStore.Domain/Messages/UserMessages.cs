using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class UserMessages
    {
        public const string NotFound = "User with the given ID is not found.";
        public const string EmailAlreadyExists = "A user with the same email already exists.";
        public const string InvalidEmail = "The provided email is invalid.";
        public const string Created = "User created successfully.";
        public const string Updated = "User updated successfully.";
        public const string Deleted = "User deleted successfully.";
    }
}
