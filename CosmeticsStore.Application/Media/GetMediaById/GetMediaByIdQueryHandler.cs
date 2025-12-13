using CosmeticsStore.Application.Media.AddMedia;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Media.GetMediaById
{
    public class GetMediaByIdQueryHandler : IRequestHandler<GetMediaByIdQuery, MediaResponse>
    {
        private readonly IMediaRepository _mediaRepository;

        public GetMediaByIdQueryHandler(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<MediaResponse> Handle(GetMediaByIdQuery request, CancellationToken cancellationToken)
        {
            var media = await _mediaRepository.GetByIdAsync(request.MediaId, cancellationToken);
            if (media == null)
                throw new MediaNotFoundException($"Media with id {request.MediaId} not found.");

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
