using System;
using Azure;
using Martyzz.Domain.Common;
using Martyzz.Domain.Repo.Interfaces;
using Martyzz.Infrastructure.Data;
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

    public async Task<PagedResult<T>> GetAll(Pagination pagination)
    {
        var page = pagination.Page;
        var pageSize = pagination.PageSize;

        var items = await _context
            .Set<T>()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var count = await _context.Set<T>().CountAsync();

        return new PagedResult<T>(items, count);
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
}
