using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Seasons.Dtos;

namespace RacingLeagueHub.Application.Seasons.Persistence;

public interface ISeasonQueries
{
    Task<SeasonDto?> GetBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        CancellationToken ct = default);

    Task<PagedResult<SeasonDto>> GetLeagueSeasonsAsync(
        string leagueSlug,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default);

    Task<PagedResult<SeasonDto>> GetLeagueSeasonsAsync(
        long leagueId,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default);
}
