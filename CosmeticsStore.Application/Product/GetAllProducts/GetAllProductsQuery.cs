using CosmeticsStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Product.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<PaginatedList<CosmeticsStore.Application.Product.AddProduct.ProductResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public Guid? CategoryId { get; set; }
        public bool? IsPublished { get; set; }
        public string? SearchTerm { get; set; }
    }
}
