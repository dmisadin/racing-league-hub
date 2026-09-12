using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.EntityHandlers;

public interface IEntityHandler
{
    int Order { get; }
    bool CanHandle(Type entityType);
    void BeforeUpdate(IEntity entity, IEntity originalEntity);
    void AfterUpdate(IEntity entity, IEntity originalEntity);
    void BeforeAdded(IEntity entity);
    void AfterAdded(IEntity entity);
    void BeforeDeleted(IEntity entity);
    void AfterDeleted(IEntity entity);
    void Validate(IEntity entity);
}
