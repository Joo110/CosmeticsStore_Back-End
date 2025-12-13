using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Review.GetAllReviews
{
    public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, PaginatedList<CosmeticsStore.Application.Review.AddReview.ReviewResponse>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetAllReviewsQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<PaginatedList<CosmeticsStore.Application.Review.AddReview.ReviewResponse>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            var query = new Query<Domain.Entities.Review>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };

            var paged = await _reviewRepository.GetForManagementAsync(query, cancellationToken);

            // Map ReviewModel -> ReviewResponse
            var items = paged.Items.Select(m => new CosmeticsStore.Application.Review.AddReview.ReviewResponse
            {
                ReviewId = m.Id,
                UserId = m.UserId,
                ProductId = m.ProductId,
                Content = m.Content,
                Rating = m.Rating,
                IsApproved = m.IsApproved,
                CreatedAtUtc = m.CreatedAtUtc
            });

            // If filters provided but repository doesn't support them, apply client-side filtering.
            // WARNING: client-side filtering will affect TotalCount/pagination accuracy; prefer implementing filtering in repository.
            if (request.ProductId.HasValue || request.UserId.HasValue || request.IsApproved.HasValue)
            {
                items = items.Where(i =>
                    (!request.ProductId.HasValue || i.ProductId == request.ProductId.Value) &&
                    (!request.UserId.HasValue || i.UserId == request.UserId.Value) &&
                    (!request.IsApproved.HasValue || i.IsApproved == request.IsApproved.Value)
                );
            }

            var itemsList = items.ToList();

            var result = new PaginatedList<CosmeticsStore.Application.Review.AddReview.ReviewResponse>(
                itemsList,
                /* TotalCount */ itemsList.Count, // if client-side filtered this is filtered count
                paged.PageIndex,
                request.PageSize
            );

            return result;
        }
    }
}
