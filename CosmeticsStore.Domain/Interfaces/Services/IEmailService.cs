using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmeticsStore.Domain.Models;
namespace CosmeticsStore.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest emailRequest, CancellationToken cancellationToken = default);
    }
}
