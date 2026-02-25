using F1StatsServer.Entities;

namespace F1StatsServer.BLL.Database.DbMaps;

public class DriverDbMap : DbMapBase<Driver>
{
    protected override string Table => "driver";
}