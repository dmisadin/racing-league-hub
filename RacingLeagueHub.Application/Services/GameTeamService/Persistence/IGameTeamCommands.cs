using RacingLeagueHub.Application.Services.GameTeamService.Dtos;

namespace RacingLeagueHub.Application.Services.GameTeamService.Persistence;

public interface IGameTeamCommands
{
    Task<GameTeamDto> AddAsync(CreateGameTeamDto dto, CancellationToken ct);
    Task<GameTeamDto?> UpdateAsync(long id, UpdateGameTeamDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(long id, CancellationToken ct);
}
