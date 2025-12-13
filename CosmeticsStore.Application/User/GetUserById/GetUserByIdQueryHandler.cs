using CosmeticsStore.Domain.Exceptions.Users;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, CosmeticsStore.Application.User.AddUser.UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CosmeticsStore.Application.User.AddUser.UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                throw new UserNotFoundException($"User with id {request.UserId} not found.");

            return new CosmeticsStore.Application.User.AddUser.UserResponse
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Roles = user.Roles?.Select(r => r.Name).ToList(),
                CreatedAtUtc = user.CreatedAtUtc,
                ModifiedAtUtc = user.ModifiedAtUtc
            };
        }
    }
}
