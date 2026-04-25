using Martyzz.Domain.Common;
using Martyzz.Domain.Common.OrderBy;
using Martyzz.Domain.Models;

namespace Martyzz.Domain.Specifications.ProductSpecs
{
    public class ProductSpecs : Specifications<Product>
    {
        public ProductSpecs(
            System.Linq.Expressions.Expression<Func<Product, bool>>? criteria = null,
            ProductSortBy? sortBy = null,
            int? page = 1,
            int? pageSize = 10
        )
            : base(criteria, page, pageSize)
        {
            Includes.Add(p => p.ProductCategory);
            Includes.Add(p => p.Brand);
            if (sortBy.HasValue)
            {
                switch (sortBy.Value)
                {
                    case ProductSortBy.NAME_ASC:
                        AddOrderBy(p => p.Name);
                        break;
                    case ProductSortBy.NAME_DESC:
                        AddOrderByDescending(p => p.Name);
                        break;
                    case ProductSortBy.PRICE_ASC:
                        AddOrderBy(p => p.Price);
                        break;
                    case ProductSortBy.PRICE_DESC:
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductSpecs(Guid id)
            : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductCategory);
            Includes.Add(p => p.Brand);
        }
    }
}
