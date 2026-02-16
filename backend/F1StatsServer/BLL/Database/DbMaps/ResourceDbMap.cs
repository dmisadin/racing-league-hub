using F1StatsServer.Entities;

namespace F1StatsServer.BLL.Database.DbMaps;

public class ResourceDbMap : BaseDbMap<Resource>
{
    protected override string Table => "resource";
}
