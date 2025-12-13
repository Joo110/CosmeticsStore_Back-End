using System;

namespace CosmeticsStore.Application.Media.AddMedia
{
    public class MediaResponse
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long SizeInBytes { get; set; }
        public bool IsPrimary { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}