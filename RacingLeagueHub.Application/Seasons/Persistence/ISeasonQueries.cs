using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Domain.Common.Models;
using RacingLeagueHub.Domain.Common.Models.Enums;

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

    Task<PagedResult<SeasonSummaryDto>> GetLeagueSeasonSummariesAsync(
        string leagueSlug,
        SeasonStatus? status = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default);

    /// <summary>Ongoing and upcoming seasons, ordered by first race (ongoing first, unscheduled last).</summary>
    Task<IReadOnlyList<SeasonSummaryDto>> GetActiveSeasonSummariesAsync(
        string leagueSlug,
        int maxCount,
        CancellationToken ct = default);

    Task<LeagueSeasonStatsDto?> GetLeagueSeasonStatsAsync(
        string leagueSlug,
        CancellationToken ct = default);

    Task<PagedResult<SeasonDto>> GetLeagueSeasonsAsync(
        int leagueId,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default);
}
