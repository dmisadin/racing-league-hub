namespace RacingLeagueHub.Domain.Entities.Stewarding;

public class Verdict : EntityBase
{
    public long IncidentId { get; set; }
    public string Summary { get; set; }
    public long Explanation { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public short StewardPenaltyType { get; set; }
    public short? PenaltyAmount { get; set; }

    public virtual Incident Incident { get; set; }
}
