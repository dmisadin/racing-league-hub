using RacingLeagueHub.BLL.Entities.Resources;

namespace RacingLeagueHub.BLL.Database.DbMaps;

public class ResourceDbMap : DbMapBase<Resource>
{
    protected override string Table => "resource";
}
