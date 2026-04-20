using System.Linq.Expressions;

namespace Martyzz.Domain.Specifications
{
    public interface ISpecifications<T>
        where T : class
    {
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, bool>>? Criteria { get; set; }
    }
}
