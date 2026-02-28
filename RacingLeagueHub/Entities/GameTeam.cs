namespace RacingLeagueHub.Entities;

public class GameTeam : EntityBase
{
    public short Game { get; set; }
    public long TeamId { get; set; }
    public string? DisplayName { get; set; }
    public string? Color { get; set; }
    public long? LogoResourceId { get; set; }
    public short TelemetryId { get; set; }

    public virtual Team Team { get; set; }
    public virtual Resource? LogoResource { get; set; }
}
