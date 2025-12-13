using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.Auth
{
    public class RegisterUserCommand : IRequest<CosmeticsStore.Application.User.Auth.AuthResponse>
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = null!; // raw password incoming from client
        public string[]? Roles { get; set; } // optional roles to assign on register (e.g. ["User"])
    }
}
