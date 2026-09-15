using RacingLeagueHub.Domain.Drivers;

namespace RacingLeagueHub.Infrastructure.DbMaps;

public class DriverDbMap : DbMapBase<Driver>
{
    protected override string Table => "driver";
}