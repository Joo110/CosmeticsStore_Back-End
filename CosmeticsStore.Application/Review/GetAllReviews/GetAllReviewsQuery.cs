using CosmeticsStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Review.GetAllReviews
{
    public class GetAllReviewsQuery : IRequest<PaginatedList<CosmeticsStore.Application.Review.AddReview.ReviewResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public Guid? ProductId { get; set; } // optional filter
        public Guid? UserId { get; set; }    // optional filter
        public bool? IsApproved { get; set; } // optional filter
    }
}
