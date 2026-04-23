using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;
using System.Linq.Expressions;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal class GenericRepository<TEntity> : IRepository<TEntity> 
    where TEntity : EntityBase
{
    protected readonly AdventureContext dbContext;

    public GenericRepository(AdventureContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public virtual IQueryable<TEntity> Query()
    {
        return this.dbContext.Set<TEntity>() as IQueryable<TEntity>;
    }

    public virtual TEntity Create()
    {
        return Activator.CreateInstance<TEntity>();
    }

    public virtual Task InsertAsync(params TEntity[] entities)
    {
        this.dbContext.Set<TEntity>().AddRange(entities);
        return Task.CompletedTask;
    }

    public virtual async ValueTask<TEntity?> FindAsync(params object[] values)
    {
        return await this.dbContext.Set<TEntity>().FindAsync(values);
    }

    public virtual async Task<TDto?> GetByIdAsync<TDto>(long id, Expression<Func<TEntity, TDto>> selector)
    {
        return await this.dbContext.Set<TEntity>()
                                .Where(x => x.Id == id )
                                .Select(selector)
                                .FirstOrDefaultAsync();
    }

    public virtual Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<TEntity, TDto>> selector)
    {
        return Query().Select(selector).ToListAsync();
    }

    public async Task<PagedResult<TDto>> GetPagedAsync<TDto>(
        Expression<Func<TEntity, TDto>> selector,
        int page,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        return await GetPagedAsync(selector, dbContext.Set<TEntity>(), page, pageSize, ct);
    }

    public async Task<PagedResult<TDto>> GetPagedAsync<TDto>(
        Expression<Func<TEntity, TDto>> selector,
        IQueryable<TEntity> query,
        int page,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        var baseQuery = query ?? dbContext.Set<TEntity>();
        var totalCount = await baseQuery.CountAsync(ct);

        var items = await baseQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(selector)
            .ToListAsync(ct);

        return new PagedResult<TDto>(items, page, pageSize, totalCount);
    }

    public virtual async Task<long?> UpdateAsync<TDto>(Func<TEntity, TDto, bool> mappingFunction, long id, TDto dto)
    {
        var entity = await FindAsync(id);

        if (entity == null)
            return null;

        mappingFunction.Invoke(entity, dto);

        await CommitAsync();

        return entity.Id;
    }

    public virtual Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return this.dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return dbContext.Set<TEntity>()
                        .Where(predicate)
                        .ExecuteDeleteAsync(cancellationToken);
    }

    public virtual Task DeleteAsync(params TEntity[] entities)
    {
        this.dbContext.Set<TEntity>().RemoveRange(entities);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(IQueryable<TEntity> query)
    {
        this.dbContext.Set<TEntity>().RemoveRange(query);
        return Task.CompletedTask;
    }
}
