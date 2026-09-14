using RacingLeagueHub.Application.Teams.Dtos;

namespace RacingLeagueHub.Application.Teams.Persistence;

public interface ITeamCommands
{
    Task<TeamDto> AddAsync(CreateTeamDto dto, CancellationToken ct);
    Task<TeamDto?> UpdateAsync(int id, UpdateTeamDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
