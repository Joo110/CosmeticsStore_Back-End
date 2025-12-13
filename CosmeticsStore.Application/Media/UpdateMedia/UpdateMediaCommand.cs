using CosmeticsStore.Application.Media.AddMedia;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Media.UpdateMedia
{
    public class UpdateMediaCommand : IRequest<MediaResponse>
    {
        public Guid MediaId { get; set; }
        public string? Url { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long? SizeInBytes { get; set; }
        public bool? IsPrimary { get; set; }
    }
}
