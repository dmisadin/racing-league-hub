using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.GameTeams.Dtos;

namespace RacingLeagueHub.Application.Teams.Dtos;

public class TeamDto : BaseDto
{
    public string Name { get; init; }
    public string? Color { get; init; }
    public List<GameTeamDto>? GameSpecificTeams { get; init; } = new List<GameTeamDto>();
}

public sealed class CreateTeamDto
{
    public string Name { get; init; }
    public string? Color { get; init; }
}

public sealed class UpdateTeamDto
{
    public string Name { get; init; }
    public string? Color { get; init; }
}