using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.BLL.Entities;

namespace RacingLeagueHub.BLL.Interceptors.EntityHandlers;

public abstract class EntityHandler<TEntity> : IEntityHandler
    where TEntity : class, IEntity
{
    public virtual int Order => 0;
    public virtual bool CanHandle(Type entityType) => typeof(TEntity).IsAssignableFrom(entityType);

    public void BeforeUpdate(IEntity entity, IEntity originalEntity, DbContext db)
    {
        BeforeUpdated(entity as TEntity, originalEntity as TEntity, db);
    }

    public void AfterUpdate(IEntity entity, IEntity originalEntity, DbContext db)
    {
        AfterUpdated(entity as TEntity, originalEntity as TEntity, db);
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

    public virtual void BeforeUpdated(TEntity entity, TEntity originalEntity, DbContext db) { }
    public virtual void AfterUpdated(TEntity entity, TEntity originalEntity, DbContext db) { }
    public virtual void BeforeAdded(TEntity entity) { }
    public virtual void AfterAdded(TEntity entity) { }
    public virtual void BeforeDeleted(TEntity entity) { }
    public virtual void AfterDeleted(TEntity entity) { }
}
