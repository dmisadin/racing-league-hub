namespace RacingLeagueHub.API.Dtos.Team;

public class TeamDto : BaseDto
{
    public string Name { get; set; }
    public string? Color { get; set; }
    public List<GameTeamDto>? GameSpecificTeams { get; set; } = new List<GameTeamDto>();
}
