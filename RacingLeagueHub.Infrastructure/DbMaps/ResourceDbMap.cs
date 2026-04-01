using RacingLeagueHub.Domain.Entities.Resources;

namespace RacingLeagueHub.Data.DbMaps;

public class ResourceDbMap : DbMapBase<Resource>
{
    protected override string Table => "resource";
}
