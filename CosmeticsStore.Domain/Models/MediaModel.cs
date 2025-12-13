using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Models
{
    public class MediaModel
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public string? ContentType { get; set; }
        public long SizeInBytes { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
