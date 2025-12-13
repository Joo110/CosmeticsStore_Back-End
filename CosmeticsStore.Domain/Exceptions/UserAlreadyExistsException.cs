using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions
{
    public class UserAlreadyExistsException : ConflictException
    {
        public override string Title => "User already exists";

        public UserAlreadyExistsException(string? message = null)
            : base(message ?? "A user with the same email already exists.") { }
    }
}
