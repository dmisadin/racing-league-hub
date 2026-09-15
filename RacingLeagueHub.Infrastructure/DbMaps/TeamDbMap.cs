using RacingLeagueHub.Domain.Teams;

namespace RacingLeagueHub.Infrastructure.DbMaps;

public class TeamDbMap : DbMapBase<Team>
{
    protected override string Table => "team";
}
