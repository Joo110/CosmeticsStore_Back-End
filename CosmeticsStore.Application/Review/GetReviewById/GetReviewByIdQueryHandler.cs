using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Review.GetReviewById
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, CosmeticsStore.Application.Review.AddReview.ReviewResponse>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewByIdQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<CosmeticsStore.Application.Review.AddReview.ReviewResponse> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);
            if (review == null)
                throw new ReviewNotFoundException($"Review with id {request.ReviewId} not found.");

            return new CosmeticsStore.Application.Review.AddReview.ReviewResponse
            {
                ReviewId = review.Id,
                UserId = review.UserId,
                ProductId = review.ProductId,
                Content = review.Content,
                Rating = review.Rating,
                IsApproved = review.IsApproved,
                CreatedAtUtc = review.CreatedAtUtc,
                ModifiedAtUtc = review.ModifiedAtUtc
            };
        }
    }
}
