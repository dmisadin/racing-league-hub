using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.GameTeamService.Dtos;
using RacingLeagueHub.Application.Services.TeamService.Dtos;

namespace RacingLeagueHub.Application.Services.GameTeamService;

public interface IGameTeamService
{
    Task<GameTeamDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<GameTeamDto>> GetPagedAsync(int page, CancellationToken ct);
    Task<GameTeamDto> AddAsync(CreateGameTeamDto dto, CancellationToken ct);
    Task<GameTeamDto?> UpdateAsync(long id, UpdateGameTeamDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(long id, CancellationToken ct);
}
