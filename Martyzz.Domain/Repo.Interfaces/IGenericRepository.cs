using Martyzz.Domain.Common;
using Martyzz.Domain.Specifications;

namespace Martyzz.Domain.Repo.Interfaces;

public interface IGenericRepository<T>
    where T : class
{
    Task<T?> Get(ISpecifications<T>? spec);
    Task<PagedResult<T>> GetAll(Pagination pagination, ISpecifications<T>? spec);
}
