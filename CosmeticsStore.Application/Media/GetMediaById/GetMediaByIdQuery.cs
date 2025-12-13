using CosmeticsStore.Application.Media.AddMedia;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Media.GetMediaById
{
    public class GetMediaByIdQuery : IRequest<MediaResponse>
    {
        public Guid MediaId { get; set; }
    }
}
