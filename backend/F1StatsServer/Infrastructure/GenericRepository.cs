using F1StatsServer.Data;
using F1StatsServer.Utility;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace F1StatsServer.Infrastructure
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly AdventureContext _context;
        private readonly DbSet<TEntity> table;

        public GenericRepository(AdventureContext context)
        {
            _context = context;
            table = _context.Set<TEntity>();
        }

        public async Task<int> CreateItemAsync(TEntity item)
        {
            await table.AddRangeAsync(item);

            var save = await Commit();

            if (save == false)
                return -1;

            return item.Id; 
        }

        public async Task<int> CreateItemListAsync(List<TEntity> items)
        {
            await table.AddRangeAsync(items);

            var save = await Commit();

            if (save == false) 
                return -1;

            return 0;
        }

        public async Task<TEntity> DeleteItemAsync(int id)
        {
            TEntity? existing = await table.FindAsync(id);

            if (existing == null)
                return null;

            table.Remove(existing);
            var result = await Commit();
            if (result == false)
                return null;

            return existing;
        }

        public async Task<List<TEntity>> GetAsync()
        {
            return await table.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var result = await table.FindAsync(id);

            return result;
        }

        public bool Has(int id)
        {
            return table.Any((c) => c.Id == id);
        }

        public async Task<int> UpdateItemAsync(JsonPatchDocument<TEntity> item, int id)
        {
            var fromDb = await _context.IncludeAll(table).Where(p => p.Id == id).FirstOrDefaultAsync();

            item.ApplyTo(fromDb);

            table.Update(fromDb);
            var save = await Commit();

            if (save == false)
                return -1;

            return 0;
        }

        public async Task<bool> Commit()
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

        /** REFACTORING BELOW */

        public virtual IQueryable<TEntity> Query()
        {
            return table;
        }

        public async Task Insert(params TEntity[] entities)
        {
            await table.AddRangeAsync(entities);
        }

        public void Remove(params TEntity[] entities)
        {
            table.RemoveRange(entities);
        }

        public void Update(params TEntity[] entities)
        {
            table.UpdateRange(entities);
        }

        //yet to be tested Upsert
        public async Task UpsertAsync<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            var exists = entity.Id != 0 && await _context.Set<TEntity>().AnyAsync(e => e.Id == entity.Id);
            if (exists)
            {
                _context.Set<TEntity>().Update(entity);
            }
            else
            {
                await _context.Set<TEntity>().AddAsync(entity);
            }
        }
    }
}
