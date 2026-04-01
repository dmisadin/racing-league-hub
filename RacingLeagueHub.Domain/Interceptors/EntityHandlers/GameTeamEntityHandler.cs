using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Interceptors.EntityHandlers.Resources;

namespace RacingLeagueHub.Domain.Interceptors.EntityHandlers;

public class GameTeamEntityHandler : EntityHandler<GameTeam>
{
    public override void BeforeUpdated(GameTeam entity, GameTeam originalEntity, DbContext db)
    {
        var resources = new EntityHandlerResourceHelper(db);

        if (originalEntity?.LogoResourceId != null
            && originalEntity.LogoResourceId != entity.LogoResourceId)
        {
            resources.MarkForDelete(originalEntity.LogoResourceId.Value);
        }

        if (entity.LogoResourceId != null)
        {
            resources.MarkActive(entity.LogoResourceId.Value);
        }
    }
}
