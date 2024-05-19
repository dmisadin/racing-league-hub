using F1StatsServer.Utility;
using Microsoft.AspNetCore.JsonPatch;

namespace F1StatsServer.Infrastructure
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        bool Has(int id);
        Task<int> CreateItemAsync(T item);
        Task<int> CreateItemListAsync(List<T> items);
        Task<T> DeleteItemAsync(int id);
        Task<bool> SaveAsync();
        Task<int> UpdateItemAsync(JsonPatchDocument<T> item, int id);
    }
}
