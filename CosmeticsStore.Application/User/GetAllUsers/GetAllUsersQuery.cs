using CosmeticsStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<PaginatedList<CosmeticsStore.Application.User.AddUser.UserResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public string? SearchTerm { get; set; }
        public string? Role { get; set; } // optional filter by role name
    }
}
