using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.DbMaps;

public class DriverDbMap : DbMapBase<Driver>
{
    protected override string Table => "driver";
}