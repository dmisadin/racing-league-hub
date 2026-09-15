using RacingLeagueHub.Domain.Teams;

namespace RacingLeagueHub.Infrastructure.Persistence.Teams;

public class TeamDbMap : DbMapBase<Team>
{
    protected override string Table => "team";
}
