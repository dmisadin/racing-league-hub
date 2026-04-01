using RacingLeagueHub.BLL.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.BLL.Infrastructure;

public interface IRepository<TEntity> where TEntity : IEntity
{
    IQueryable<TEntity> Query();
    TEntity Create();
    ValueTask<TEntity?> FindAsync(params object[] values);
    Task<TDto?> GetByIdAsync<TDto>(long id, Expression<Func<TEntity, TDto>> selector);
    Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<TEntity, TDto>> selector);
    Task InsertAsync(params TEntity[] entities);
    Task<long?> UpdateAsync<TDto>(Func<TEntity, TDto, bool> mappingFunction, long id, TDto dto);
    Task<int> CommitAsync();
    Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task DeleteAsync(params TEntity[] entities);
    Task DeleteAsync(IQueryable<TEntity> query);
}
