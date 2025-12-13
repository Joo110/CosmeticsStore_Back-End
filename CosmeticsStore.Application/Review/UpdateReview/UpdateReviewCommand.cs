using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Review.UpdateReview
{
    public class UpdateReviewCommand : IRequest<CosmeticsStore.Application.Review.AddReview.ReviewResponse>
    {
        public Guid ReviewId { get; set; }

        // partial update
        public string? Content { get; set; }
        public int? Rating { get; set; }
        public bool? IsApproved { get; set; }
    }
}
