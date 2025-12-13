using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Review.AddReview
{
    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, CosmeticsStore.Application.Review.AddReview.ReviewResponse>
    {
        private readonly IReviewRepository _reviewRepository;

        public AddReviewCommandHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<CosmeticsStore.Application.Review.AddReview.ReviewResponse> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var review = new CosmeticsStore.Domain.Entities.Review
            {
                UserId = request.UserId,
                ProductId = request.ProductId,
                Content = request.Content,
                Rating = request.Rating,
                IsApproved = request.IsApproved,
                CreatedAtUtc = DateTime.UtcNow
            };

            var created = await _reviewRepository.CreateAsync(review, cancellationToken);

            return new CosmeticsStore.Application.Review.AddReview.ReviewResponse
            {
                ReviewId = created.Id,
                UserId = created.UserId,
                ProductId = created.ProductId,
                Content = created.Content,
                Rating = created.Rating,
                IsApproved = created.IsApproved,
                CreatedAtUtc = created.CreatedAtUtc,
                ModifiedAtUtc = created.ModifiedAtUtc
            };
        }
    }
}
