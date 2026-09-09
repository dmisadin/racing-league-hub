using RacingLeagueHub.Application.Leagues.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Seasons.Dtos;

namespace RacingLeagueHub.Application.Leagues.Persistence;

public interface ILeagueQueries
{
    Task<LeagueDto?> GetBySlugAsync(string leagueSlug, CancellationToken ct = default);
    Task<PagedResult<LeagueDto>> GetLeaguesAsync(
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default);
}
