using System.ComponentModel.DataAnnotations;

namespace CosmeticsStore.Domain.Models
{
    public class Attachment
    {
        [Required]
        public string FileName { get; set; } = null!;

        [Required]
        public byte[] File { get; set; } = null!;

        public string MediaType { get; set; } = "application";

        public string SubMediaType { get; set; } = "octet-stream";
    }
}