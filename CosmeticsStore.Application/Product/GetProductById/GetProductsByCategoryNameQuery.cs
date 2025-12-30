using MediatR;
using System.Collections.Generic;

namespace CosmeticsStore.Application.Product.GetProductsByCategoryName
{
    public class GetProductsByCategoryNameQuery
        : IRequest<IEnumerable<CosmeticsStore.Application.Product.AddProduct.ProductResponse>>
    {
        public string CategoryName { get; }

        public GetProductsByCategoryNameQuery(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
