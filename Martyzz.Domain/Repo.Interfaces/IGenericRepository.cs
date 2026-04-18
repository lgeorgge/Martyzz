using System;
using Martyzz.Domain.Common;

namespace Martyzz.Domain.Repo.Interfaces;

public interface IGenericRepository<T>
{
    Task<T?> GetById(Guid id);
    Task<PagedResult<T>> GetAll(Pagination pagination);
}
