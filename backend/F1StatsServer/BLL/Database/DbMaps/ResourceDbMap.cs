using F1StatsServer.Entities;

namespace F1StatsServer.BLL.Database.DbMaps;

public class ResourceDbMap : DbMapBase<Resource>
{
    protected override string Table => "resource";
}
