using Martyzz.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Martyzz.Infrastructure.Specifications
{
    public class SpecificationEvaluator<T>
        where T : class
    {
        public static IQueryable<T> GetQuery(
            IQueryable<T> inputQuery,
            ISpecifications<T>? spec,
            bool skipPagination = false
        )
        {
            var query = inputQuery;

            if (spec?.Criteria != null)
                query = query.Where(spec.Criteria);
            query =
                spec?.Includes?.Aggregate(query, (current, include) => current.Include(include))
                ?? query;

            // Add pagination
            if (!skipPagination && spec != null && spec.Page != null && spec.PageSize != null)
            {
                var skip = spec.Page.HasValue ? (spec.Page.Value - 1) * spec.PageSize.Value : 0;
                var take = spec.PageSize.Value;
                query = query.Skip(skip).Take(take);
            }
            // Add ordering
            if (spec?.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec?.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            return query;
        }
    }
}
