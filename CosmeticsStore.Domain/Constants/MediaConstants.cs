using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Constants
{
    public static class MediaConstants
    {
        public const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB
        public const int MaxFileNameLength = 200;
        public const int UrlMaxLength = 2000;
        public const int ContentTypeMaxLength = 100;

        // Allowed types as comma-separated string for simple checks (or use config)
        public const string AllowedImageContentTypes = "image/jpeg,image/png,image/webp,image/gif";
    }
}
