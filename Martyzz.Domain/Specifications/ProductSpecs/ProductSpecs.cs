using Martyzz.Domain.Models;

namespace Martyzz.Domain.Specifications.ProductSpecs
{
    public class ProductSpecs : Specifications<Product>
    {
        public ProductSpecs(System.Linq.Expressions.Expression<Func<Product, bool>>? criteria)
            : base(criteria)
        {
            Includes.Add(p => p.ProductCategory);
            Includes.Add(p => p.Brand);
        }

        public ProductSpecs(Guid id)
            : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductCategory);
            Includes.Add(p => p.Brand);
        }
    }
}
