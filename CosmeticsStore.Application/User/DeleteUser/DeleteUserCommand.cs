using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
    }
}
