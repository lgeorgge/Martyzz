using System.Linq.Expressions;
using Martyzz.Domain.Common;

namespace Martyzz.Domain.Specifications
{
    public class Specifications<T> : ISpecifications<T>
        where T : class
    {
        public List<Expression<Func<T, object>>> Includes { get; set; } = [];
        public Expression<Func<T, bool>>? Criteria { get; set; } = null;
        public Expression<Func<T, object>>? OrderBy { get; set; }
        public Expression<Func<T, object>>? OrderByDescending { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }

        public Specifications(
            Expression<Func<T, bool>>? criteria,
            int? page = 1,
            int? pageSize = 10
        )
        {
            Criteria = criteria;
            Includes = [];
            Page = page < 1 ? 1 : page;
            PageSize = pageSize < 1 || pageSize > 100 ? 10 : pageSize;
        }

        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
    }
}
