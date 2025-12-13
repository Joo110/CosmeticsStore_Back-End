using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Exceptions.Users;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, CosmeticsStore.Application.User.AddUser.UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<CosmeticsStore.Domain.Entities.User> _passwordHasher;
        private readonly IRoleRepository? _roleRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository,
                                        IPasswordHasher<CosmeticsStore.Domain.Entities.User> passwordHasher,
                                        IRoleRepository? roleRepository = null)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }


        public async Task<CosmeticsStore.Application.User.AddUser.UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                throw new UserNotFoundException($"User with id {request.UserId} not found.");

            if (request.FullName != null) user.FullName = request.FullName;
            if (request.PhoneNumber != null) user.PhoneNumber = request.PhoneNumber;
            if (!string.IsNullOrEmpty(request.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            }
            if (request.Roles != null && _roleRepository != null)
            {
                // simple replace policy
                user.Roles.Clear();
                foreach (var roleName in request.Roles.Distinct())
                {
                    var role = await _roleRepository.GetByNameAsync(roleName, cancellationToken)
                        ?? await _roleRepository.CreateAsync(new Role { Name = roleName }, cancellationToken);
                    user.Roles.Add(role);
                }
            }

            user.ModifiedAtUtc = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user, cancellationToken);

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
