using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Models
{
    public record PaginationMetadata(
  int TotalItemCount,
  int CurrentPage,
  int PageSize)
    {
        public int TotalPageCount => (int)Math.Ceiling((double)TotalItemCount / PageSize);
    }
}
