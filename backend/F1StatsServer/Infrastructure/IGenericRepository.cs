using F1StatsServer.Util;
using Microsoft.AspNetCore.JsonPatch;

namespace F1StatsServer.Infrastructure
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        IQueryable<T> Get();
        T GetById(int id);
        bool Has(int id);
        int CreateItem(T item);
        int CreateItemList(List<T> items);
        T DeleteItem(int id);
        bool Save();
        int UpdateItem(JsonPatchDocument<T> item, int id);
    }
}
