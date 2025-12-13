using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Review.GetReviewById
{
    public class GetReviewByIdQuery : IRequest<CosmeticsStore.Application.Review.AddReview.ReviewResponse>
    {
        public Guid ReviewId { get; set; }
    }
}
