using RacingLeagueHub.BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RacingLeagueHub.BLL.Database.DbMaps;

public abstract class DbMapBase<TEntity> : IDbMap
    where TEntity : class, IEntity
{
    public virtual void Initialize(ModelBuilder modelBuilder)
    {
        var entityTypeBuilder = modelBuilder.Entity<TEntity>();

        entityTypeBuilder.ToTable(this.Table.ToLower(), this.Schema.ToLower());
        entityTypeBuilder.HasKey(x => x.Id);

        Map(entityTypeBuilder);
    }

    protected virtual void Map(EntityTypeBuilder<TEntity> builder) { }

    protected abstract string Table { get; }
    protected virtual string Schema => "public";
}
