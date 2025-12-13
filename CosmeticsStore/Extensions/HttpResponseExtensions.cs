using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace CosmeticsStore.Extensions
{
    public static class HttpResponseExtensions
    {
        private const string PaginationHeaderName = "X-Pagination";

        public static void AddPaginationMetadata(this IHeaderDictionary headers, object metadata)
        {
            var json = JsonSerializer.Serialize(metadata);
            headers[PaginationHeaderName] = json;
            // expose header for CORS if needed by client (optional)
            // headers["Access-Control-Expose-Headers"] = PaginationHeaderName;
        }

        public static void AddPaginationMetadata(this HttpResponse response, object metadata)
            => response.Headers.AddPaginationMetadata(metadata);
    }
}
