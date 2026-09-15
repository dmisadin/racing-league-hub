using RacingLeagueHub.Domain.Drivers;

namespace RacingLeagueHub.Infrastructure.Persistence.Drivers;

public class DriverDbMap : DbMapBase<Driver>
{
    protected override string Table => "driver";
}