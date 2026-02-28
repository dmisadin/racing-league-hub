using RacingLeagueHub.Entities;

namespace RacingLeagueHub.BLL.Database.DbMaps;

public class DriverDbMap : DbMapBase<Driver>
{
    protected override string Table => "driver";
}