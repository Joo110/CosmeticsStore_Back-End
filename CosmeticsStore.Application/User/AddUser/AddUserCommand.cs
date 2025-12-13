using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.AddUser
{
    public class AddUserCommand : IRequest<CosmeticsStore.Application.User.AddUser.UserResponse>
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; } // optional when admin creates user; hashed if provided
        public string[]? Roles { get; set; }
    }
}
