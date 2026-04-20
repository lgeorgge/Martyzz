using Martyzz.Domain.Common;
using Martyzz.Domain.Repo.Interfaces;
using Martyzz.Domain.Specifications;
using Martyzz.Infrastructure.Data;
using Martyzz.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Martyzz.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : class
{
    private readonly StoreDbContext _context;

    public GenericRepository(StoreDbContext storeDbContext)
    {
        _context = storeDbContext;
    }

    public async Task<PagedResult<T>> GetAll(Pagination pagination, ISpecifications<T>? spec)
    {
        var page = pagination.Page;
        var pageSize = pagination.PageSize;

        var items = await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), spec)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var count = await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), spec)
            .CountAsync();

        return new PagedResult<T>(items, count);
    }

    public async Task<T?> Get(ISpecifications<T>? spec)
    {
        return await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), spec)
            .FirstOrDefaultAsync();
    }
}
