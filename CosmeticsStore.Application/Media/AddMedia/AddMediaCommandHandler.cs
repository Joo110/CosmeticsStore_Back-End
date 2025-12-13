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
            var media = new CosmeticsStore.Domain.Entities.Media
            {
                OwnerId = request.OwnerId,
                Url = request.Url,
                FileName = request.FileName,
                ContentType = request.ContentType,
                SizeInBytes = request.SizeInBytes,
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
