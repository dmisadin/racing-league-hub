using RacingLeagueHub.BLL.Entities;

namespace RacingLeagueHub.BLL.Interceptors.EntityHandlers;

public abstract class EntityHandler<TEntity> : IEntityHandler
    where TEntity : class, IEntity
{
    public virtual int Order => 0;

    public virtual void AfterAdded(IEntity entity)
    {
    }

    public virtual void AfterDeleted(IEntity entity)
    {
    }

    public virtual void AfterUpdate(IEntity entity, IEntity originalEntity)
    {
    }

    public virtual void BeforeAdded(IEntity entity)
    {
    }

    public virtual void BeforeDeleted(IEntity entity)
    {
    }

    public virtual void BeforeUpdate(IEntity entity, IEntity originalEntity)
    {
    }

    public virtual void Validate(IEntity entity)
    {
    }

    public virtual bool CanHandle(Type entityType) => typeof(TEntity).IsAssignableFrom(entityType);
}
