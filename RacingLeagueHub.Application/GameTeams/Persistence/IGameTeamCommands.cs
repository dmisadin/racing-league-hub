using RacingLeagueHub.Application.GameTeams.Dtos;

namespace RacingLeagueHub.Application.GameTeams.Persistence;

public interface IGameTeamCommands
{
    Task<GameTeamDto> AddAsync(CreateGameTeamDto dto, CancellationToken ct);
    Task<GameTeamDto?> UpdateAsync(int id, UpdateGameTeamDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
