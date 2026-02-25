using F1StatsServer.Entities;

namespace F1StatsServer.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> Query();
        TEntity Create();
        ValueTask<TEntity?> FindAsync(params object[] values);
        Task<List<TEntity>> GetAllAsync();
        Task InsertAsync(params TEntity[] entities);
        Task<int> CommitAsync();
        Task DeleteAsync(params TEntity[] entities);
        Task DeleteAsync(IQueryable<TEntity> query);
    }
}
