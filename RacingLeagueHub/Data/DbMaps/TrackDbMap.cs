using RacingLeagueHub.BLL.Entities;

namespace RacingLeagueHub.Data.DbMaps;

public class TrackDbMap : DbMapBase<Track>
{
    protected override string Table => "track";
}
