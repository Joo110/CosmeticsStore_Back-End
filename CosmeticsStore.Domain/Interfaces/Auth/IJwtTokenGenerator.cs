using CosmeticsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmeticsStore.Domain.Models;
namespace CosmeticsStore.Domain.Interfaces.Auth
{
    public interface IJwtTokenGenerator
    {
        JwtToken Generate(User user);
    }
}
