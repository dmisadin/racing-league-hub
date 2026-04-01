using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.DbMaps;

public class TrackDbMap : DbMapBase<Track>
{
    protected override string Table => "track";
}
