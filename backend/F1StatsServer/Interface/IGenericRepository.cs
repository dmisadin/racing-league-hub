namespace F1StatsServer.Interface
{
    public interface IGenericRepository<T>
    {
        List<T> Get();
        T GetById(int id);
        bool Has(int id);
        //List<T> CreateItem(T item);
        //List<T> DeleteItem(int id);
    }
}
