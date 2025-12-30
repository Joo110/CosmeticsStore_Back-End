using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Media.AddMedia
{
    public class AddMediaCommandHandler : IRequestHandler<AddMediaCommand, MediaResponse>
    {
        private readonly IMediaRepository _mediaRepository;

        public AddMediaCommandHandler(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<MediaResponse> Handle(AddMediaCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;

            // حفظ الصورة على القرص
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var uploadPath = Path.Combine("wwwroot", "uploads", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(uploadPath)!);

            using var stream = new FileStream(uploadPath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            var media = new CosmeticsStore.Domain.Entities.Media
            {
                OwnerId = request.OwnerId,
                Url = $"/uploads/{fileName}",
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
