using Martyzz.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Martyzz.Infrastructure.Specifications
{
    public class SpecificationEvaluator<T>
        where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T>? spec)
        {
            var query = inputQuery;
            if (spec?.Criteria != null)
                query = query.Where(spec.Criteria);
            query =
                spec?.Includes?.Aggregate(query, (current, include) => current.Include(include))
                ?? query;
            return query;
        }
    }
}
