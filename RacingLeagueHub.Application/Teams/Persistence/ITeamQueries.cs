using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Teams.Dtos;

namespace RacingLeagueHub.Application.Teams.Persistence;

public interface ITeamQueries
{
    Task<TeamDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<TeamDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
}
