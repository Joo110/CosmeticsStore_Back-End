using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Media.AddMedia
{
    public class AddMediaCommand : IRequest<MediaResponse>
    {
        public Guid OwnerId { get; set; }
        public IFormFile File { get; set; } = default!;
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long SizeInBytes { get; set; }
        public bool IsPrimary { get; set; }
    }
}
