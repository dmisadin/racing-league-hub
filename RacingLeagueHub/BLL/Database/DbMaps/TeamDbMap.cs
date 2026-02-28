using F1StatsServer.Entities;

namespace F1StatsServer.BLL.Database.DbMaps;

public class TeamDbMap : DbMapBase<Team>
{
    protected override string Table => "team";
}
