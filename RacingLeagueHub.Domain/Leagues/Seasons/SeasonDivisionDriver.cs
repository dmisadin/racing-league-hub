using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Drivers;
using RacingLeagueHub.Domain.Teams;

namespace RacingLeagueHub.Domain.Leagues.Seasons;

public class SeasonDivisionDriver : EntityBase
{
    public int SeasonDivisionId { get; set; }
    public int TeamId { get; set; }
    public int DriverId { get; set; }
    public short? RacingNumber { get; set; }
    public short PenaltyPoints { get; set; }

    public virtual SeasonDivision SeasonDivision { get; set; }
    public virtual Team Team { get; set; }
    public virtual Driver Driver { get; set; }
}
