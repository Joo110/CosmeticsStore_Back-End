using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Media.DeleteMedia
{
    public class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommand, Unit>
    {
        private readonly IMediaRepository _mediaRepository;

        public DeleteMediaCommandHandler(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<Unit> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            var media = await _mediaRepository.GetByIdAsync(request.MediaId, cancellationToken);
            if (media == null)
                throw new MediaNotFoundException($"Media with id {request.MediaId} not found.");

            await _mediaRepository.DeleteAsync(request.MediaId, cancellationToken);
            return Unit.Value;
        }
    }
}
