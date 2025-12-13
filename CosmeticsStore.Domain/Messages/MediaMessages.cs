using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class MediaMessages
    {
        public const string NotFound = "Media with the given ID is not found.";
        public const string InvalidFile = "The uploaded file is not a valid media type.";
        public const string UploadFailed = "Failed to upload the media file.";
        public const string Deleted = "Media deleted successfully.";
    }
}
