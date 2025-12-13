using CosmeticsStore.Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Exceptions.Users;

public class UserNotFoundException : NotFoundException
{
    public override string Title => "User not found";

    public UserNotFoundException(string? message = null)
        : base(message ?? "User with the given ID was not found.") { }
}