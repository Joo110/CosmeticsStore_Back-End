using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.GetUserById
{
    public class GetUserByIdQuery : IRequest<CosmeticsStore.Application.User.AddUser.UserResponse>
    {
        public Guid UserId { get; set; }
    }
}
