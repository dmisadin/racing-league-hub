using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Util;
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

        public bool CreateItem(T item)
        {
            table.Add(item);

            return Save();
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
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
