using RacingLeagueHub.Domain.Models.Enums.Assists;

namespace RacingLeagueHub.Domain.Entities.Seasons;

public class SeasonAssists : EntityBase
{
    public long SeasonId { get; set; }
    public RacingLine RacingLine { get; set; }
    public Gearbox Gearbox { get; set; }
    public TractionControl TractionControl { get; set; }
    public bool Abs { get; set; }

    public virtual Season Season { get; set; }
}
