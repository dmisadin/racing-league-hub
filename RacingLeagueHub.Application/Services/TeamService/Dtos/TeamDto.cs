using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Dtos.Team;

namespace RacingLeagueHub.Application.Services.TeamService.Dtos;

public class TeamDto : BaseDto
{
    public string Name { get; set; }
    public string? Color { get; set; }
    public List<GameTeamDto>? GameSpecificTeams { get; set; } = new List<GameTeamDto>();
}

public sealed class CreateTeamDto
{
    public string Name { get; init; }
    public string? Color { get; set; }
}
public sealed class UpdateTeamDto
{
    public string Name { get; init; }
    public string? Color { get; set; }
}