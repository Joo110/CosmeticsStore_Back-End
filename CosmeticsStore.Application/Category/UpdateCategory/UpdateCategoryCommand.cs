using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Category.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Unit>
    {
        public Guid CategoryId { get; }
        public string Name { get; }
        public string? Description { get; }
        public int? ParentCategoryId { get; }
        public bool IsActive { get; }

        public UpdateCategoryCommand(Guid categoryId, string name, string? description, int? parentCategoryId, bool isActive)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
            IsActive = isActive;
        }
    }
}
