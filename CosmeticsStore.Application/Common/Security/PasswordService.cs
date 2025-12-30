using CosmeticsStore.Application.Common.Security;
using Microsoft.AspNetCore.Identity;

namespace CosmeticsStore.Application.Common.Security
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<CosmeticsStore.Domain.Entities.User> _passwordHasher;

        public PasswordService(IPasswordHasher<CosmeticsStore.Domain.Entities.User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string Hash(CosmeticsStore.Domain.Entities.User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool Verify(CosmeticsStore.Domain.Entities.User user, string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}