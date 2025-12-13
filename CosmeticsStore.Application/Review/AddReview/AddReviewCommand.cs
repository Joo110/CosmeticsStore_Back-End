using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Review.AddReview
{
    public class AddReviewCommand : IRequest<CosmeticsStore.Application.Review.AddReview.ReviewResponse>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; } // 1..5
        public bool IsApproved { get; set; } = false;
    }
}
