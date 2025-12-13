using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CosmeticsStore.Infrastructure.Persistence.Services.Images
{
    public class FirebaseImageService : IImageService
    {
        private static readonly string[] AllowedImageFormats = { ".jpg", ".jpeg", ".png" };
        private readonly FirebaseConfig _firebaseConfig;

        public FirebaseImageService(IOptions<FirebaseConfig> fireBaseConfig)
        {
            _firebaseConfig = fireBaseConfig.Value ?? throw new ArgumentNullException(nameof(fireBaseConfig));
        }

        public async Task<Media> StoreAsync(IFormFile image, CancellationToken cancellationToken = default)
        {
            if (image == null || image.Length <= 0)
                throw new ArgumentNullException(nameof(image));

            var imageFormat = Path.GetExtension(image.FileName).ToLower();

            if (!AllowedImageFormats.Contains(imageFormat))
                throw new ArgumentOutOfRangeException($"Allowed formats: {string.Join(", ", AllowedImageFormats)}");

            var credential = GoogleCredential.FromJson(_firebaseConfig.CredentialsJson);
            var storage = StorageClient.Create(credential);

            var fileName = Guid.NewGuid().ToString() + imageFormat;

            using var stream = image.OpenReadStream();
            await storage.UploadObjectAsync(
                bucket: _firebaseConfig.Bucket,
                objectName: fileName,
                contentType: image.ContentType,
                source: stream,
                cancellationToken: cancellationToken
            );

            var media = new Media
            {
                OwnerId = Guid.Empty,
                Url = $"https://storage.googleapis.com/{_firebaseConfig.Bucket}/{fileName}",
                FileName = fileName,
                ContentType = image.ContentType,
                SizeInBytes = image.Length,
                IsPrimary = false,
                CreatedAtUtc = DateTime.UtcNow
            };

            return media;
        }

        public async Task DeleteAsync(Media media, CancellationToken cancellationToken = default)
        {
            if (media == null) throw new ArgumentNullException(nameof(media));

            var credential = GoogleCredential.FromJson(_firebaseConfig.CredentialsJson);
            var storage = StorageClient.Create(credential);

            await storage.DeleteObjectAsync(
                bucket: _firebaseConfig.Bucket,
                objectName: media.FileName,
                cancellationToken: cancellationToken
            );
        }
    }
}
