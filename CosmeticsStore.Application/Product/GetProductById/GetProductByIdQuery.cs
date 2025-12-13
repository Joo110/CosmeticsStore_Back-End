using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Product.GetProductById
{
    public class GetProductByIdQuery : IRequest<CosmeticsStore.Application.Product.AddProduct.ProductResponse>
    {
        public Guid ProductId { get; set; }
    }
}
