using MediatR;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;

namespace CosmeticsStore.Application.Category.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;

        public AddCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Domain.Entities.Category
            {
                Id = Guid.NewGuid(), 
                Name = request.Name,
                Description = request.Description,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _categoryRepository.CreateAsync(category, cancellationToken);
            return category.Id;
        }
    }
}
