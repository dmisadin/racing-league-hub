using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.GameTeamService.Dtos;

namespace RacingLeagueHub.Application.Services.GameTeamService.Persistence;

public interface IGameTeamQueries
{
    Task<GameTeamDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<GameTeamDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
}
