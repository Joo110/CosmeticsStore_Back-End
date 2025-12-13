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
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<CosmeticsStore.Domain.Entities.User> _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(IUserRepository userRepository,
                                   IPasswordHasher<CosmeticsStore.Domain.Entities.User> passwordHasher,
                                   IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }


        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
                throw new InvalidOperationException("Invalid credentials.");

            if (string.IsNullOrEmpty(user.PasswordHash) ||
                _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) != PasswordVerificationResult.Success)
            {
                throw new InvalidOperationException("Invalid credentials.");
            }

            var roles = user.Roles?.Select(r => r.Name).ToArray() ?? Array.Empty<string>();
            var token = _jwtService.GenerateToken(
                user.Id,
                user.Email,
                roles
            );

            return new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Token = token,
            };
        }

    }
}
