using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Media.AddMedia
{
    public class AddMediaCommandHandler : IRequestHandler<AddMediaCommand, MediaResponse>
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IWebHostEnvironment _env;

        public AddMediaCommandHandler(IMediaRepository mediaRepository, IWebHostEnvironment env)
        {
            _mediaRepository = mediaRepository;
            _env = env;
        }

        public async Task<MediaResponse> Handle(AddMediaCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);
            var uploadPath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(uploadPath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            var url = $"/uploads/{fileName}";

            var media = new CosmeticsStore.Domain.Entities.Media
            {
                OwnerId = request.OwnerId,
                Url = url,
                FileName = file.FileName,
                ContentType = file.ContentType,
                SizeInBytes = file.Length,
                IsPrimary = request.IsPrimary,
                CreatedAtUtc = DateTime.UtcNow
            };

            var created = await _mediaRepository.CreateAsync(media, cancellationToken);

            return new MediaResponse
            {
                Id = created.Id,
                OwnerId = created.OwnerId,
                Url = created.Url,
                FileName = created.FileName,
                ContentType = created.ContentType,
                SizeInBytes = created.SizeInBytes,
                IsPrimary = created.IsPrimary,
                CreatedAtUtc = created.CreatedAtUtc,
                ModifiedAtUtc = created.ModifiedAtUtc
            };
        }
    }
}
