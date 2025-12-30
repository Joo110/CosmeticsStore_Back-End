using CosmeticsStore.Application.Common.Security;
using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.Auth
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        public RegisterUserCommandHandler(
     IUserRepository userRepository,
     IPasswordService passwordService,
     IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }


        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var exists = await _userRepository.ExistsAsync(u => u.Email == request.Email, cancellationToken);
            if (exists)
                throw new InvalidOperationException("Email already exists.");

            var user = new CosmeticsStore.Domain.Entities.User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                CreatedAtUtc = DateTime.UtcNow,
                IsActive = true,
                IsEmailConfirmed = true,
                Roles = new List<Role>()
            };

            user.PasswordHash = _passwordService.Hash(user, request.Password);

            if (request.Roles != null)
            {
                foreach (var roleName in request.Roles.Distinct())
                {
                    user.Roles.Add(new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = roleName
                    });
                }
            }

            await _userRepository.CreateAsync(user, cancellationToken);

            var token = _jwtService.GenerateToken(
                user.Id,
                user.Email,
                user.Roles.Select(r => r.Name)
            );

            return new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Token = token
            };
        }

    }
}
