using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Infrastructure.Persistence;
using RacingLeagueHub.Infrastructure.Persistence.EntityHandlers;
using RacingLeagueHub.Infrastructure.Persistence.EntityHandlers.Resources;

namespace RacingLeagueHub.Domain.Interceptors.EntityHandlers;

internal class GameTeamEntityHandler : EntityHandler<GameTeam>
{
    public GameTeamEntityHandler(RacingContext racingContext) : base(racingContext)
    {
    }

    public override void BeforeUpdated(GameTeam entity, GameTeam originalEntity)
    {
        var resources = new EntityHandlerResourceHelper(racingContext);

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
