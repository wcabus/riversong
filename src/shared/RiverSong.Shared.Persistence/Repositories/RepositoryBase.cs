using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RiverSong.Shared.Application.Contracts;
using RiverSong.Shared.Application.Models;
using RiverSong.Shared.Domain.Common;

namespace RiverSong.Shared.Persistence.Repositories;

public abstract class RepositoryBase<T, TContext> : IAsyncRepository<T>
    where T : EntityBase
    where TContext : DbContext
{
    protected RepositoryBase(TContext dbContext)
    {
        DbContext = dbContext;
    }

    protected TContext DbContext { get; }


    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? whereExpression = null)
    {
        var query = DbContext.Set<T>().AsQueryable().AsNoTracking();
        if (whereExpression != null)
        {
            query = query.Where(whereExpression);
        }

        return await query.ToListAsync();
    }
    
    public async Task<Page<T>> GetPageAsync(int page, int pageSize, Expression<Func<T, bool>>? whereExpression = null)
    {
        return await InternalGetPageAsync(page, pageSize, whereExpression);
    }

    private async Task<Page<T>> InternalGetPageAsync(int page, int pageSize, Expression<Func<T, bool>>? whereExpression = null, Expression<Func<T, object>>? orderByExpression = null)
    {
        var query = DbContext.Set<T>().AsQueryable().AsNoTracking();

        if (whereExpression != null)
        {
            query = query.Where(whereExpression);
        }

        var totalItems = await query.CountAsync();

        // Need to order by something when paging
        var items = await query
            .OrderBy(orderByExpression ?? (x => x.Id))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new Page<T>(items, page, pageSize, totalItems);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public Task<T> UpdatedAsync(T entity)
    {
        DbContext.Set<T>().Update(entity);
        return Task.FromResult(entity);
    }

    public Task RemoveAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
        {
            return;
        }

        DbContext.Remove(entity);
    }
}