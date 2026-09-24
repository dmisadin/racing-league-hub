using RacingLeagueHub.Application.Teams.Dtos;
using RacingLeagueHub.Domain.Common.Models;

namespace RacingLeagueHub.Application.Teams.Persistence;

public interface ITeamQueries
{
    Task<TeamDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<PagedResult<TeamDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
}
