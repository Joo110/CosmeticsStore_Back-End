using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Interfaces.Persistence.Repositories
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string email, IEnumerable<string> roles, TimeSpan? expiry = null);
    }
}
