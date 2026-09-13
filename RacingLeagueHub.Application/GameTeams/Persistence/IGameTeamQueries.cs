using RacingLeagueHub.Application.GameTeams.Dtos;
using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.GameTeams.Persistence;

public interface IGameTeamQueries
{
    Task<GameTeamDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<GameTeamDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
}
