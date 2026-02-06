using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Utility
{
    public static class DbContextExtension
    {
        public static IQueryable<TEntity> IncludeAll<TEntity>(this DbContext context, DbSet<TEntity> dbSet)
            where TEntity : class
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var navigationProperty in context.Model.FindEntityType(typeof(TEntity)).GetNavigations())
            {
                query = query.Include(navigationProperty.Name);
            }

            return query;
        }
    }
}

