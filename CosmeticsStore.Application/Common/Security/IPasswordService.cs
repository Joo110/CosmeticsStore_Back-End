using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Common.Security
{
    public interface IPasswordService
    {
        string Hash(Domain.Entities.User user, string password);
        bool Verify(Domain.Entities.User user, string hashedPassword, string providedPassword);
    }
}