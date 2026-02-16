using F1StatsServer.Entities;

namespace F1StatsServer.BLL.Database.DbMaps;

public class TeamDbMap : BaseDbMap<Team>
{
    protected override string Table => "team";
}
