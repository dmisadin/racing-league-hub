using F1StatsServer.Entities;

namespace F1StatsServer.BLL.Database.DbMaps;

public class TrackDbMap : DbMapBase<Track>
{
    protected override string Table => "track";
}
