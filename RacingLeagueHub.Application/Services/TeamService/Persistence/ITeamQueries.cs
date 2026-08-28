using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.TeamService.Dtos;

namespace RacingLeagueHub.Application.Services.TeamService.Persistence;

public interface ITeamQueries
{
    Task<TeamDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<TeamDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
}
