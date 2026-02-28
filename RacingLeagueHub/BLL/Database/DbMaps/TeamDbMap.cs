using RacingLeagueHub.Entities;

namespace RacingLeagueHub.BLL.Database.DbMaps;

public class TeamDbMap : DbMapBase<Team>
{
    protected override string Table => "team";
}
