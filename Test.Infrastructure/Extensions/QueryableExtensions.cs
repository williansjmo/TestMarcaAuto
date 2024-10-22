using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Test.Domain.Dto;
using System.Linq.Dynamic.Core;

namespace Test.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDto pagination)
        {
            if (pagination.Page == 0)
                return queryable;

            var q = queryable
                .Skip((pagination.Page - 1) * pagination.RecordsNumber)
                .Take(pagination.RecordsNumber);
            if (pagination != null && !string.IsNullOrEmpty(pagination.SortColumn))
            {
                q = q.OrderBy(pagination.SortColumn + " " + pagination.SortDirection);
            }
            return q;
        }
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }
            return query;
        }
    }
}
