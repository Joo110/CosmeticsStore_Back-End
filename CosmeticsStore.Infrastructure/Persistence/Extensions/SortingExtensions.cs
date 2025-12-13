using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Infrastructure.Persistence.Extensions
{
    public static class SortingExtensions
    {
        public static IQueryable<TItem> Sort<TItem>(
          this IQueryable<TItem> queryable,
          Expression<Func<TItem, object>> sortColumnExpression,
          SortOrder sortOrder)
        {
            return sortOrder switch
            {
                SortOrder.Ascending => queryable.OrderBy(sortColumnExpression),
                SortOrder.Descending => queryable.OrderByDescending(sortColumnExpression),
                _ => throw new InvalidEnumArgumentException()
            };
        }
    }
}
