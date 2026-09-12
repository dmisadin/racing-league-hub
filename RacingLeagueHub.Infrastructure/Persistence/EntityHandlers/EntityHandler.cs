
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.EntityHandlers;

internal abstract class EntityHandler<TEntity> : IEntityHandler
    where TEntity : class, IEntity
{
    protected readonly RacingContext racingContext;

    public EntityHandler(RacingContext racingContext)
    {
        this.racingContext = racingContext;
    }

    public virtual int Order => 0;
    public virtual bool CanHandle(Type entityType) => typeof(TEntity).IsAssignableFrom(entityType);

    public void BeforeUpdate(IEntity entity, IEntity originalEntity)
    {
        BeforeUpdated(entity as TEntity, originalEntity as TEntity);
    }

    public void AfterUpdate(IEntity entity, IEntity originalEntity)
    {
        AfterUpdated(entity as TEntity, originalEntity as TEntity);
    }

    public void BeforeAdded(IEntity entity)
    {
        BeforeAdded(entity as TEntity);
    }

    public void AfterAdded(IEntity entity)
    {
        AfterAdded(entity as TEntity);
    }

    public void BeforeDeleted(IEntity entity)
    {
        BeforeDeleted(entity as TEntity);
    }

    public void AfterDeleted(IEntity entity)
    {
        AfterDeleted(entity as TEntity);
    }

    public void Validate(IEntity entity)
    {
    }

    public virtual void BeforeUpdated(TEntity entity, TEntity originalEntity) { }
    public virtual void AfterUpdated(TEntity entity, TEntity originalEntity) { }
    public virtual void BeforeAdded(TEntity entity) { }
    public virtual void AfterAdded(TEntity entity) { }
    public virtual void BeforeDeleted(TEntity entity) { }
    public virtual void AfterDeleted(TEntity entity) { }
}
