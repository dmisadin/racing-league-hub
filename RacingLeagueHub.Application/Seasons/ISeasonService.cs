using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Domain.Common.Models;
using RacingLeagueHub.Domain.Common.Models.Enums;

namespace RacingLeagueHub.Application.Seasons;

public interface ISeasonService
{
    Task<LeagueSeasonsOverviewDto?> GetLeagueOverviewAsync(
        string leagueSlug,
        int completedPageSize = 10,
        CancellationToken ct = default);

    Task<PagedResult<SeasonSummaryDto>> GetLeagueSeasonSummariesAsync(
        string leagueSlug,
        SeasonStatus? status,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default);
}
