using RacingLeagueHub.Application.Leagues.Persistence;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Application.Seasons.Persistence;
using RacingLeagueHub.Domain.Common.Models;
using RacingLeagueHub.Domain.Common.Models.Enums;

namespace RacingLeagueHub.Application.Seasons;

internal class SeasonService : ISeasonService
{
    private readonly ISeasonQueries seasonQueries;
    private readonly ISeasonCommands seasonCommands;
    private readonly ILeagueQueries leagueQueries;

    public SeasonService(
        ISeasonQueries seasonQueries,
        ISeasonCommands seasonCommands,
        ILeagueQueries leagueQueries)
    {
        this.seasonQueries = seasonQueries;
        this.seasonCommands = seasonCommands;
        this.leagueQueries = leagueQueries;
    }

    public async Task<LeagueSeasonsOverviewDto?> GetLeagueOverviewAsync(
        string leagueSlug,
        int completedPageSize = 10,
        CancellationToken ct = default)
    {
        var stats = await seasonQueries.GetLeagueSeasonStatsAsync(leagueSlug, ct);

        if (stats is null)
            return null;

        var active = await seasonQueries.GetActiveSeasonSummariesAsync(leagueSlug, ct);
        var completed = await seasonQueries.GetLeagueSeasonSummariesAsync(
            leagueSlug, SeasonStatus.Completed, 1, completedPageSize, ct);

        // Every running season is current; when nothing is running, the soonest upcoming one is featured instead.
        var ongoing = active.Where(s => s.Status == SeasonStatus.Ongoing).ToList();
        var current = ongoing.Count > 0
            ? ongoing
            : active.Take(1).ToList();

        return new LeagueSeasonsOverviewDto
        {
            SeasonCount = stats.SeasonCount,
            RacesHeld = stats.RacesHeld,
            CurrentSeasons = current,
            UpcomingSeasons = active
                .Where(s => s.Status == SeasonStatus.Upcoming && !current.Contains(s))
                .ToList(),
            CompletedSeasons = completed
        };
    }

    public Task<PagedResult<SeasonSummaryDto>> GetLeagueSeasonSummariesAsync(
        string leagueSlug,
        SeasonStatus? status,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        return seasonQueries.GetLeagueSeasonSummariesAsync(leagueSlug, status, page, pageSize, ct);
    }

    public async Task<SeasonDetailsDto?> GetDetailsAsync(
        string leagueSlug,
        string seasonSlug,
        CancellationToken ct = default)
    {
        var details = await seasonQueries.GetDetailsAsync(leagueSlug, seasonSlug, ct);

        if (details is null)
            return null;

        details.Divisions = await seasonQueries.GetDivisionSummariesAsync(details.Season.Id, ct);

        return details;
    }
}
