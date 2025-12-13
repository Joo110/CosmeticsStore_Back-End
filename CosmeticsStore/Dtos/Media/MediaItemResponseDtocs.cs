namespace CosmeticsStore.Dtos.Media
{
    public class MediaItemResponseDto
    {
        public Guid MediaItemId { get; set; }
        public string FileName { get; set; } = default!;
        public string Url { get; set; } = default!;
        public string? MediaType { get; set; }
        public long Size { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
