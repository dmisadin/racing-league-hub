using RacingLeagueHub.Domain.Common.Entitites;

namespace RacingLeagueHub.Domain.Stewarding;

public class Verdict : EntityBase
{
    public int IncidentId { get; set; }
    public string Summary { get; set; }
    public int Explanation { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public short StewardPenaltyType { get; set; }
    public short? PenaltyAmount { get; set; }

    public virtual Incident Incident { get; set; }
}
