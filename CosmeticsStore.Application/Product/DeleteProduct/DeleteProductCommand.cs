using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Product.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public Guid ProductId { get; set; }
    }
}
