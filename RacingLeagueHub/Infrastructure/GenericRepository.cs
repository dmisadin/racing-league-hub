using RacingLeagueHub.BLL.Database;
using RacingLeagueHub.BLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace RacingLeagueHub.Infrastructure;

public class GenericRepository<TEntity> : IRepository<TEntity> 
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

    public virtual ValueTask<TEntity?> FindAsync(params object[] values)
    {
        return this.dbContext.Set<TEntity>().FindAsync(values);
    }

    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return Query().ToListAsync();
    }

    public virtual Task<int> CommitAsync()
    {
        return this.dbContext.SaveChangesAsync();
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
