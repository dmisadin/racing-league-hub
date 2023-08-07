using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace F1StatsServer.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly AdventureContext _context;
        private readonly DbSet<T> table;

        public GenericRepository(AdventureContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public int CreateItem(T item)
        {
            table.Add(item);

            var save = Save();

            if (save == false)
                return -1;

            return item.Id; 
        }

        public int CreateItemList(List<T> items)
        {
            table.AddRange(items);

            var save = Save();

            if(save == false) 
                return -1;

            return 0;
        }

        public T DeleteItem(int id)
        {
            T? existing = table.Find(id);

            table.Remove(existing);
            Save();
            return existing;
        }

        public IQueryable<T> Get()
        {
            return table;
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public bool Has(int id)
        {
            return table.Any((c) => c.Id == id);
        }

        public bool Save()
        {
            try
            {
                var saved = _context.SaveChanges();
                return saved > 0;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("UC"))
                    throw new Exception("Duplicate value inserted");
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateItem(T item)
        {
            table.Update(item);
            var save = Save();

            if (save == false)
                return -1;

            return 0;
        }
    }
}
