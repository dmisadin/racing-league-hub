using RacingLeagueHub.Application.GameTeams.Dtos;
using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.GameTeams;

public interface IGameTeamService
{
    Task<GameTeamDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<PagedResult<GameTeamDto>> GetPagedAsync(int page, CancellationToken ct);
    Task<GameTeamDto> AddAsync(CreateGameTeamDto dto, CancellationToken ct);
    Task<GameTeamDto?> UpdateAsync(int id, UpdateGameTeamDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
