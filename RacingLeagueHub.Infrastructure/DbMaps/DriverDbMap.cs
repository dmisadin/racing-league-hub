using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Data.DbMaps;

public class DriverDbMap : DbMapBase<Driver>
{
    protected override string Table => "driver";
}