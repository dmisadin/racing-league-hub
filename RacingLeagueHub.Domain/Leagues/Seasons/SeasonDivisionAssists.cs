using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Common.Models.Enums.Assists;

namespace RacingLeagueHub.Domain.Leagues.Seasons;

public class SeasonDivisionAssists : EntityBase
{
    public int SeasonDivisionId { get; set; }
    public RacingLine RacingLine { get; set; }
    public Gearbox Gearbox { get; set; }
    public TractionControl TractionControl { get; set; }
    public bool Abs { get; set; }

    public virtual SeasonDivision SeasonDivision { get; set; }
}
