using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<CosmeticsStore.Application.User.AddUser.UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginatedList<CosmeticsStore.Application.User.AddUser.UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var query = new Query<Domain.Entities.User>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm
            };

            var paged = await _userRepository.GetForManagementAsync(query, cancellationToken);

            var items = paged.Items.Select(m => new CosmeticsStore.Application.User.AddUser.UserResponse
            {
                UserId = m.Id,
                Email = m.Email,
                FullName = m.FullName,
                PhoneNumber = m.PhoneNumber,
                Roles = null,
                CreatedAtUtc = m.CreatedAtUtc,
                ModifiedAtUtc = m.ModifiedAtUtc
            });

            if (!string.IsNullOrWhiteSpace(request.Role))
            {
                items = items.Where(u => u.Roles != null && u.Roles.Contains(request.Role));
            }

            var itemsList = items.ToList();

            var result = new PaginatedList<CosmeticsStore.Application.User.AddUser.UserResponse>(
                itemsList,
                itemsList.Count, // if server filtering not used this is filtered count
                paged.PageIndex,
                request.PageSize
            );

            return result;
        }
    }
}
