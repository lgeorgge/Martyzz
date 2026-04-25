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

    public async Task<PaginatedResult<T>> GetAll(ISpecifications<T>? spec)
    {
        var items = await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), spec)
            .ToListAsync();
        var count = await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), spec, true)
            .CountAsync();

        return new PaginatedResult<T>(
            items,
            count,
            spec?.Page ?? 1,
            spec?.PageSize ?? 10,
            (spec?.Page ?? 1) * (spec?.PageSize ?? 10) < count
        );
    }

    public async Task<T?> Get(ISpecifications<T>? spec)
    {
        return await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), spec)
            .FirstOrDefaultAsync();
    }
}
