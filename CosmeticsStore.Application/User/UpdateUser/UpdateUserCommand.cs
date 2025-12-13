using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.UpdateUser
{
    public class UpdateUserCommand : IRequest<CosmeticsStore.Application.User.AddUser.UserResponse>
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; } // optional: will be hashed
        public string[]? Roles { get; set; } // replace roles if provided
    }
}
