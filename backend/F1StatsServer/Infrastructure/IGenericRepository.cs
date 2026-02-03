using F1StatsServer.Utility;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace F1StatsServer.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        Task<List<TEntity>> GetAsync();
        Task<TEntity> GetByIdAsync(int id);
        bool Has(int id);
        Task<int> CreateItemAsync(TEntity item);
        Task<int> CreateItemListAsync(List<TEntity> items);
        Task<TEntity> DeleteItemAsync(int id);
        Task<bool> Commit();
        Task<int> UpdateItemAsync(JsonPatchDocument<TEntity> item, int id);
        Task UpsertAsync<TEntity>(TEntity entity) where TEntity : EntityBase;

        IQueryable<TEntity> Query();
        Task Insert(params TEntity[] entities);
        void Remove(params TEntity[] entities);
        void Update(params TEntity[] entities);
    }
}
