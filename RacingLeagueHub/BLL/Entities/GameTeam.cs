using RacingLeagueHub.BLL.Models.Enums;

namespace RacingLeagueHub.BLL.Entities;

public class GameTeam : EntityBase
{
    public Game Game { get; set; }
    public long TeamId { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Abbreviation { get; set; }
    public string? Color { get; set; }
    public long? LogoResourceId { get; set; }
    public short TelemetryId { get; set; }

    public virtual Team Team { get; set; }
    public virtual Resource? LogoResource { get; set; }
}
