using RacingLeagueHub.BLL.Entities;

namespace RacingLeagueHub.Data.DbMaps;

public class TeamDbMap : DbMapBase<Team>
{
    protected override string Table => "team";
}
