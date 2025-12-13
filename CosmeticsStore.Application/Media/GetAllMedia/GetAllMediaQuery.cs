using CosmeticsStore.Application.Media.AddMedia;
using CosmeticsStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Media.GetAllMedia
{
    public class GetAllMediaQuery : IRequest<PaginatedList<MediaResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public Guid? OwnerId { get; set; } // optional filter
    }
}
