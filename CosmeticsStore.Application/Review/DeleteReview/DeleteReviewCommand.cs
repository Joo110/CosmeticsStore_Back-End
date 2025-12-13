using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Review.DeleteReview
{
    public class DeleteReviewCommand : IRequest<Unit>
    {
        public Guid ReviewId { get; set; }
    }
}
