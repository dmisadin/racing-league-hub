using RacingLeagueHub.Domain.Entities.Resources;

namespace RacingLeagueHub.Infrastructure.DbMaps;

public class ResourceDbMap : DbMapBase<Resource>
{
    protected override string Table => "resource";
}
