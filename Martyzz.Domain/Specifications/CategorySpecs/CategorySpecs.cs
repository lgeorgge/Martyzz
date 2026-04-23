using System;
using System.Linq.Expressions;
using Martyzz.Domain.Models;

namespace Martyzz.Domain.Specifications.CategorySpecs;

public class CategorySpecs : Specifications<Category>
{
    public CategorySpecs(Expression<Func<Category, bool>>? criteria)
        : base(criteria) { }

    public CategorySpecs(int id)
        : base(c => c.Id == id) { }
}
