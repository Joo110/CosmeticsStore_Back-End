using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Review.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, CosmeticsStore.Application.Review.AddReview.ReviewResponse>
    {
        private readonly IReviewRepository _reviewRepository;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<CosmeticsStore.Application.Review.AddReview.ReviewResponse> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);
            if (review == null)
                throw new ReviewNotFoundException($"Review with id {request.ReviewId} not found.");

            if (request.Content != null) review.Content = request.Content;
            if (request.Rating.HasValue) review.Rating = request.Rating.Value;
            if (request.IsApproved.HasValue) review.IsApproved = request.IsApproved.Value;

            review.ModifiedAtUtc = DateTime.UtcNow;

            await _reviewRepository.UpdateAsync(review, cancellationToken);

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
