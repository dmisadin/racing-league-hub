using RacingLeagueHub.Application.Services.TeamService.Dtos;

namespace RacingLeagueHub.Application.Services.TeamService.Persistence;

public interface ITeamCommands
{
    Task<TeamDto> AddAsync(CreateTeamDto dto, CancellationToken ct);
    Task<TeamDto?> UpdateAsync(long id, UpdateTeamDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(long id, CancellationToken ct);
}
