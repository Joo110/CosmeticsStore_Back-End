using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, CosmeticsStore.Application.User.AddUser.UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<CosmeticsStore.Domain.Entities.User> _passwordHasher;
        private readonly IRoleRepository? _roleRepository;

        public AddUserCommandHandler(IUserRepository userRepository, IPasswordHasher<CosmeticsStore.Domain.Entities.User> passwordHasher, IRoleRepository? roleRepository = null)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }

        public async Task<UserResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = new CosmeticsStore.Domain.Entities.User
            {
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                CreatedAtUtc = DateTime.UtcNow
            };

            // hash password
            if (!string.IsNullOrEmpty(request.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            }

            // assign roles if any
            if (request.Roles != null && request.Roles.Any() && _roleRepository != null)
            {
                foreach (var roleName in request.Roles.Distinct())
                {
                    var role = await _roleRepository.GetByNameAsync(roleName, cancellationToken)
                               ?? await _roleRepository.CreateAsync(new Role { Name = roleName }, cancellationToken);
                    user.Roles.Add(role);
                }
            }

            var created = await _userRepository.CreateAsync(user, cancellationToken);

            return new UserResponse
            {
                UserId = created.Id,
                Email = created.Email,
                FullName = created.FullName,
                PhoneNumber = created.PhoneNumber,
                Roles = created.Roles?.Select(r => r.Name).ToList()
            };
        }

    }
}
