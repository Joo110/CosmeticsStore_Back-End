using CosmeticsStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Interfaces.Persistence.Services
{
    public interface IImageService
    {
        Task<Media> StoreAsync(
          IFormFile image,
          CancellationToken cancellationToken = default);

        Task DeleteAsync(
          Media image,
          CancellationToken cancellationToken = default);
    }
}
