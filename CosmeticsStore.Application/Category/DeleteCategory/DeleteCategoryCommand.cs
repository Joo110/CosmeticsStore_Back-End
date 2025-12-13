using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Category.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public Guid CategoryId { get; }

        public DeleteCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
