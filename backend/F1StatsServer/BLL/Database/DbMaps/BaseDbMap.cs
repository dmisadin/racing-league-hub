using F1StatsServer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1StatsServer.BLL.Database.DbMaps;

public abstract class BaseDbMap<TEntity> where TEntity : class, IEntity
{
    public virtual void Initialize(ModelBuilder modelBuilder)
    {
        var entityTypeBuilder = modelBuilder.Entity<TEntity>();

        var builder = entityTypeBuilder.ToTable(this.Table.ToLower(), this.Schema.ToLower())
                                       .HasKey(x => x.Id);

        Map(entityTypeBuilder);
    }

    protected virtual void Map(EntityTypeBuilder<TEntity> builder) { }

    protected abstract string Table { get; }
    protected virtual string Schema => "public";
}
