using RacingLeagueHub.BLL.Entities;

namespace RacingLeagueHub.Data.DbMaps;

public class DriverDbMap : DbMapBase<Driver>
{
    protected override string Table => "driver";
}