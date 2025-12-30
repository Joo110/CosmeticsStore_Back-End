using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.User.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserResponse>
    {
        private readonly AppDbContext _db;
        private readonly IPasswordHasher<CosmeticsStore.Domain.Entities.User> _passwordHasher;

        public AddUserCommandHandler(AppDbContext db, IPasswordHasher<CosmeticsStore.Domain.Entities.User> passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (existingUser != null)
            {
                throw new InvalidOperationException($"User with email {request.Email} already exists.");
            }

            var user = new CosmeticsStore.Domain.Entities.User
            {
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                CreatedAtUtc = DateTime.UtcNow,
                Roles = new List<Role>()
            };

            // Hash password
            if (!string.IsNullOrEmpty(request.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            }

            if (request.Roles != null && request.Roles.Any())
            {
                foreach (var roleName in request.Roles.Distinct())
                {
                    var role = await _db.Roles
                        .FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);

                    if (role == null)
                    {
                        role = new Role { Name = roleName };
                        _db.Roles.Add(role);
                        await _db.SaveChangesAsync(cancellationToken);
                    }

                    user.Roles.Add(role);
                }
            }

            _db.Users.Add(user);
            await _db.SaveChangesAsync(cancellationToken);

            var finalUser = await _db.Users
                .Include(u => u.Roles)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);

            if (finalUser == null)
                throw new Exception("User not found after creation.");

            return new UserResponse
            {
                UserId = finalUser.Id,
                Email = finalUser.Email,
                FullName = finalUser.FullName,
                PhoneNumber = finalUser.PhoneNumber,
                Roles = finalUser.Roles.Select(r => r.Name).ToList(),
                CreatedAtUtc = finalUser.CreatedAtUtc,
                ModifiedAtUtc = finalUser.ModifiedAtUtc
            };
        }
    }
}