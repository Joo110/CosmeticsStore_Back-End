using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class ReviewMessages
    {
        public const string NotFound = "Review with the given ID is not found.";
        public const string NotApproved = "Review is not approved.";
        public const string InvalidRating = "Rating must be between the allowed range.";
        public const string Created = "Review submitted successfully.";
        public const string Approved = "Review approved.";
        public const string Deleted = "Review deleted successfully.";
    }
}
