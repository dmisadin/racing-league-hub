using F1StatsServer.Util;

namespace F1StatsServer.Infrastructure
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        IQueryable<T> Get();
        T GetById(int id);
        bool Has(int id);
        bool CreateItem(T item);
        T DeleteItem(int id);
        bool Save();
    }
}
