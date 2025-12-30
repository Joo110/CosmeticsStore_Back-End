using CosmeticsStore.Application.Category.AddCategory;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Category.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetWithProductsAsync(cancellationToken);
            var result = new List<CategoryResponse>();

            foreach (var category in categories)
            {
                result.Add(new CategoryResponse
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    CreatedAtUtc = category.CreatedAtUtc
                });
            }

            return result;
        }
    }
}
