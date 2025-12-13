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
        private readonly IPasswordHasher<CosmeticsStore.Domain.Entities.User> _passwordHasher;
        private readonly IJwtService _jwtService;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher<CosmeticsStore.Domain.Entities.User> passwordHasher,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // check existing
            var exists = await _userRepository.ExistsAsync(u => u.Email == request.Email, cancellationToken);
            if (exists)
                throw new InvalidOperationException($"User with email {request.Email} already exists.");

            // create user
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

            // ✅ تعيين Roles مباشرة بدون Repository
            if (request.Roles != null && request.Roles.Length > 0)
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

            var created = await _userRepository.CreateAsync(user, cancellationToken);

            // generate jwt
            var token = _jwtService.GenerateToken(
                created.Id,
                created.Email,
                created.Roles?.Select(r => r.Name) ?? Array.Empty<string>()
            );

            return new AuthResponse
            {
                UserId = created.Id,
                Email = created.Email,
                FullName = created.FullName,
                Token = token,
            };
        }
    }
}
