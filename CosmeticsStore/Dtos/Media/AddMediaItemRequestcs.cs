namespace CosmeticsStore.Dtos.Media
{
    public class AddMediaItemRequest
    {
        public string FileName { get; set; } = default!;
        public string Url { get; set; } = default!;
        public string? MediaType { get; set; } // e.g. "image/png"
        public long Size { get; set; } // bytes
    }
}
