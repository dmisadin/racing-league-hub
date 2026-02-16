using F1StatsServer.Entities;

namespace F1StatsServer.BLL.Database.DbMaps;

public class DriverDbMap : BaseDbMap<Driver>
{
    protected override string Table => "driver";
}