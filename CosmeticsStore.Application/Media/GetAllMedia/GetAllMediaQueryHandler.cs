using CosmeticsStore.Application.Media.AddMedia;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Media.GetAllMedia
{
    public class GetAllMediaQueryHandler : IRequestHandler<GetAllMediaQuery, PaginatedList<MediaResponse>>
    {
        private readonly IMediaRepository _mediaRepository;

        public GetAllMediaQueryHandler(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<PaginatedList<MediaResponse>> Handle(GetAllMediaQuery request, CancellationToken cancellationToken)
        {
            var query = new Query<CosmeticsStore.Domain.Entities.Media>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            var paged = await _mediaRepository.GetForManagementAsync(query, cancellationToken);


            var responseItems = paged.Items.Select(m => new MediaResponse
            {
                Id = m.Id,
                OwnerId = m.OwnerId,
                Url = m.Url,
                FileName = m.FileName,
                ContentType = m.ContentType,
                SizeInBytes = m.SizeInBytes,
                IsPrimary = m.IsPrimary,
                CreatedAtUtc = m.CreatedAtUtc,
                ModifiedAtUtc = m.ModifiedAtUtc
            });

            var result = new PaginatedList<MediaResponse>(
                responseItems,
                paged.TotalCount,
                paged.PageIndex,
                request.PageSize
            );

            return result;
        }
    }
}
