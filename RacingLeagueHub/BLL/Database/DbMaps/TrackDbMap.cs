using RacingLeagueHub.Entities;

namespace RacingLeagueHub.BLL.Database.DbMaps;

public class TrackDbMap : DbMapBase<Track>
{
    protected override string Table => "track";
}
