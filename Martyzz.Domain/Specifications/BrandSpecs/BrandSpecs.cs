using System;
using System.Linq.Expressions;
using Martyzz.Domain.Models;

namespace Martyzz.Domain.Specifications.BrandSpecs;

public class BrandSpecs : Specifications<Brand>
{
    public BrandSpecs(Expression<Func<Brand, bool>>? criteria)
        : base(criteria) { }

    public BrandSpecs(int id)
        : base(b => b.Id == id) { }
}
