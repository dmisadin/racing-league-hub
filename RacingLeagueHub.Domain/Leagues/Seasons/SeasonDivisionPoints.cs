using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Common.Models.Enums;

namespace RacingLeagueHub.Domain.Leagues.Seasons;

public class SeasonDivisionPoints : EntityBase
{
    public int SeasonDivisionId { get; set; }
    public SessionType SessionType { get; set; }
    public short Position { get; set; }
    public short Points { get; set; }

    public virtual SeasonDivision SeasonDivision { get; set; }
}
