using System.Linq.Expressions;

namespace Martyzz.Domain.Specifications
{
    public class Specifications<T> : ISpecifications<T>
        where T : class
    {
        public List<Expression<Func<T, object>>> Includes { get; set; } = [];
        public Expression<Func<T, bool>>? Criteria { get; set; } = null;

        public Specifications(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
            Includes = [];
        }
    }
}
