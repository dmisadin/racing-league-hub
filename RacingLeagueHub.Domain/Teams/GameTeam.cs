using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Models.Enums;
using RacingLeagueHub.Domain.Resources;

namespace RacingLeagueHub.Domain.Teams;

public class GameTeam : EntityBase
{
    public Game Game { get; set; }
    public int TeamId { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Abbreviation { get; set; }
    public string? Color { get; set; }
    public int? LogoResourceId { get; set; }
    public short TelemetryId { get; set; }

    public virtual Team Team { get; set; }
    public virtual Resource? LogoResource { get; set; }
}
