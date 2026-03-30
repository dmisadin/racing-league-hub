using RacingLeagueHub.BLL.Entities;

namespace RacingLeagueHub.BLL.Interceptors.EntityHandlers;

public interface IEntityHandler
{
    int Order { get; }
    void BeforeUpdate(IEntity entity, IEntity originalEntity);
    void AfterUpdate(IEntity entity, IEntity originalEntity);
    void BeforeAdded(IEntity entity);
    void AfterAdded(IEntity entity);
    void BeforeDeleted(IEntity entity);
    void AfterDeleted(IEntity entity);
    void Validate(IEntity entity);
    bool CanHandle(Type entityType);
}
