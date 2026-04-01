using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Interceptors.EntityHandlers.Resources;

namespace RacingLeagueHub.BLL.Interceptors.EntityHandlers;

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
