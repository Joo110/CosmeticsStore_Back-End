using CosmeticsStore.Application.Media.AddMedia;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Media.UpdateMedia
{
    public class UpdateMediaCommandHandler : IRequestHandler<UpdateMediaCommand, MediaResponse>
    {
        private readonly IMediaRepository _mediaRepository;

        public UpdateMediaCommandHandler(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<MediaResponse> Handle(UpdateMediaCommand request, CancellationToken cancellationToken)
        {
            var media = await _mediaRepository.GetByIdAsync(request.MediaId, cancellationToken);
            if (media == null)
                throw new MediaNotFoundException($"Media with id {request.MediaId} not found.");

            if (request.Url != null) media.Url = request.Url;
            if (request.FileName != null) media.FileName = request.FileName;
            if (request.ContentType != null) media.ContentType = request.ContentType;
            if (request.SizeInBytes.HasValue) media.SizeInBytes = request.SizeInBytes.Value;
            if (request.IsPrimary.HasValue) media.IsPrimary = request.IsPrimary.Value;

            media.ModifiedAtUtc = DateTime.UtcNow;

            await _mediaRepository.UpdateAsync(media, cancellationToken);

            return new MediaResponse
            {
                Id = media.Id,
                OwnerId = media.OwnerId,
                Url = media.Url,
                FileName = media.FileName,
                ContentType = media.ContentType,
                SizeInBytes = media.SizeInBytes,
                IsPrimary = media.IsPrimary,
                CreatedAtUtc = media.CreatedAtUtc,
                ModifiedAtUtc = media.ModifiedAtUtc
            };
        }
    }
}
