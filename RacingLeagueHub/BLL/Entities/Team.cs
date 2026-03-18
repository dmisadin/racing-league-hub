using RacingLeagueHub.BLL.Entities.Seasons;

namespace RacingLeagueHub.BLL.Entities;

public class Team : EntityBase
{
    public string Name { get; set; }
    public string? Color { get; set; }

    public virtual ICollection<GameTeam> GameTeams { get; set; } = new List<GameTeam>();
    public virtual ICollection<GrandPrixDriver> GrandPrixDrivers { get; set; } = new List<GrandPrixDriver>();
    public virtual ICollection<SeasonDriver> SeasonDrivers { get; set; } = new List<SeasonDriver>();
}