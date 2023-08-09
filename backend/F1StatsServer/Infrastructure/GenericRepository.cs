using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Util;
using Microsoft.AspNetCore.JsonPatch;
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

        public async Task<int> CreateItemAsync(T item)
        {
            await table.AddRangeAsync(item);

            var save = await SaveAsync();

            if (save == false)
                return -1;

            return item.Id; 
        }

        public async Task<int> CreateItemListAsync(List<T> items)
        {
            await table.AddRangeAsync(items);

            var save = await SaveAsync();

            if (save == false) 
                return -1;

            return 0;
        }

        public async Task<T> DeleteItemAsync(int id)
        {
            T? existing = await table.FindAsync(id);

            if (existing == null)
                return null;

            table.Remove(existing);
            var result = await SaveAsync();
            if (result == false)
                return null;

            return existing;
        }

        public async Task<List<T>> GetAsync()
        {
            return await table.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await table.FindAsync(id);

            return result ?? null;
        }

        public bool Has(int id)
        {
            return table.Any((c) => c.Id == id);
        }

        public async Task<int> UpdateItemAsync(JsonPatchDocument<T> item, int id)
        {
            var fromDb = await _context.IncludeAll(_context.Set<T>()).Where(p => p.Id == id).FirstOrDefaultAsync();

            item.ApplyTo(fromDb);

            table.Update(fromDb);
            var save = await SaveAsync();

            if (save == false)
                return -1;

            return 0;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                var saved = await _context.SaveChangesAsync();
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
    }
}
