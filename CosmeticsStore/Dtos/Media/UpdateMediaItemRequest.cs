namespace CosmeticsStore.Dtos.Media
{
    public class UpdateMediaItemRequest
    {
        public string? FileName { get; set; }
        public string? Url { get; set; }
        public string? MediaType { get; set; }
        public long? Size { get; set; }
    }
}
