using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Domain.Interceptors.EntityHandlers;

public interface IEntityHandler
{
    int Order { get; }
    bool CanHandle(Type entityType);
    void BeforeUpdate(IEntity entity, IEntity originalEntity, DbContext db);
    void AfterUpdate(IEntity entity, IEntity originalEntity, DbContext db);
    void BeforeAdded(IEntity entity);
    void AfterAdded(IEntity entity);
    void BeforeDeleted(IEntity entity);
    void AfterDeleted(IEntity entity);
    void Validate(IEntity entity);
}
