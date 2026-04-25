using System.Linq.Expressions;

namespace Martyzz.Domain.Specifications
{
    public interface ISpecifications<T>
        where T : class
    {
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, bool>>? Criteria { get; set; }

        public Expression<Func<T, object>>? OrderBy { get; set; }
        public Expression<Func<T, object>>? OrderByDescending { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        public void AddOrderBy(Expression<Func<T, object>> orderByExpression);

        public void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression);
    }
}
