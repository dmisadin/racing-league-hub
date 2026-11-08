using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Application.Seasons.Persistence;
using RacingLeagueHub.Domain.Common.Models;
using RacingLeagueHub.Domain.Common.Models.Enums;
using RacingLeagueHub.Domain.Leagues.Seasons;
using RacingLeagueHub.Domain.Leagues.Seasons.Rules;
using RacingLeagueHub.Infrastructure.Extensions;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons;

internal class SeasonQueries : ISeasonQueries
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<Season, SeasonDto> mapper;

    public SeasonQueries(RacingContext racingContext,
        IDtoMapper<Season, SeasonDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<SeasonDto?> GetBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        CancellationToken ct = default)
    {
        return await racingContext.Season
            .Where(s => s.Slug == seasonSlug && s.League.Slug == leagueSlug)
            .Select(mapper.ToDtoExpression())
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PagedResult<SeasonDto>> GetLeagueSeasonsAsync(
        string leagueSlug,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        return await racingContext.Season
            .Where(s => s.League.Slug == leagueSlug)
            .Select(mapper.ToDtoExpression())
            .ToPagedResultAsync(page, pageSize, ct);
    }

    public async Task<PagedResult<SeasonSummaryDto>> GetLeagueSeasonSummariesAsync(
        string leagueSlug,
        SeasonStatus? status = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        var now = DateTimeOffset.UtcNow;

        var query = racingContext.Season
            .AsNoTracking()
            .Where(s => s.League.Slug == leagueSlug);

        if (status is SeasonStatus statusFilter)
            query = query.Where(SeasonStatusRules.HasStatus(statusFilter, now));

        var seasons = await OrderForStatus(query, status)
            .Select(mapper.ToDtoExpression())
            .ToPagedResultAsync(page, pageSize, ct);

        var items = await ToSummariesAsync(seasons.Items, now, ct);

        return new PagedResult<SeasonSummaryDto>(items, seasons.Page, seasons.PageSize, seasons.TotalCount);
    }

    public async Task<IReadOnlyList<SeasonSummaryDto>> GetActiveSeasonSummariesAsync(
        string leagueSlug,
        CancellationToken ct = default)
    {
        var now = DateTimeOffset.UtcNow;

        // Ongoing seasons have their first race in the past, so they sort before upcoming ones.
        // PostgreSQL sorts NULLs last in ascending order, which puts unscheduled seasons at the end.
        var seasons = await racingContext.Season
            .AsNoTracking()
            .Where(s => s.League.Slug == leagueSlug)
            .Where(SeasonStatusRules.IsActive(now))
            .OrderBy(s => s.GrandsPrix.Min(gp => (DateTimeOffset?)gp.StartingAt))
            .ThenByDescending(s => s.Id)
            .Select(mapper.ToDtoExpression())
            .ToListAsync(ct);

        return await ToSummariesAsync(seasons, now, ct);
    }

    public async Task<LeagueSeasonStatsDto?> GetLeagueSeasonStatsAsync(
        string leagueSlug,
        CancellationToken ct = default)
    {
        var now = DateTimeOffset.UtcNow;

        return await racingContext.League
            .AsNoTracking()
            .Where(l => l.Slug == leagueSlug)
            .Select(l => new LeagueSeasonStatsDto
            {
                SeasonCount = l.Seasons.Count(),
                RacesHeld = l.Seasons
                    .SelectMany(s => s.GrandsPrix)
                    .Count(gp => gp.StartingAt <= now)
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PagedResult<SeasonDto>> GetLeagueSeasonsAsync(
        int leagueId,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        return await racingContext.Season
            .Where(s => s.LeagueId == leagueId)
            .Select(mapper.ToDtoExpression())
            .ToPagedResultAsync(page, pageSize, ct);
    }

    private static IQueryable<Season> OrderForStatus(IQueryable<Season> query, SeasonStatus? status)
    {
        return status switch
        {
            SeasonStatus.Completed => query
                .OrderByDescending(s => s.GrandsPrix.Max(gp => (DateTimeOffset?)gp.StartingAt))
                .ThenByDescending(s => s.Id),
            SeasonStatus.Upcoming => query
                .OrderBy(s => s.GrandsPrix.Min(gp => (DateTimeOffset?)gp.StartingAt))
                .ThenByDescending(s => s.Id),
            _ => query.OrderByDescending(s => s.Id)
        };
    }

    private async Task<List<SeasonSummaryDto>> ToSummariesAsync(
        IReadOnlyList<SeasonDto> seasons,
        DateTimeOffset now,
        CancellationToken ct)
    {
        if (seasons.Count == 0)
            return [];

        var seasonIds = seasons.Select(s => s.Id).ToList();

        var stats = await racingContext.Season
            .AsNoTracking()
            .Where(s => seasonIds.Contains(s.Id))
            .Select(s => new
            {
                s.Id,
                RoundsTotal = s.GrandsPrix.Count(),
                RoundsCompleted = s.GrandsPrix.Count(gp => gp.StartingAt <= now),
                DriverCount = s.SeasonDrivers.Count(),
                FirstRaceAt = s.GrandsPrix.Min(gp => (DateTimeOffset?)gp.StartingAt),
                LastRaceAt = s.GrandsPrix.Max(gp => (DateTimeOffset?)gp.StartingAt),
                NextGrandPrix = s.GrandsPrix
                    .Where(gp => gp.StartingAt > now)
                    .OrderBy(gp => gp.StartingAt)
                    .Select(gp => new SeasonNextGrandPrixDto
                    {
                        Name = gp.Name,
                        Slug = gp.Slug,
                        StartingAt = gp.StartingAt,
                        TrackName = gp.TrackLayout.Track.Name,
                        TrackCity = gp.TrackLayout.Track.City
                    })
                    .FirstOrDefault()
            })
            .ToDictionaryAsync(x => x.Id, ct);

        // One row per (season, driver, team); driver and constructor totals are both folded from it.
        var standings = await racingContext.GrandPrixResult
            .AsNoTracking()
            .Where(r => seasonIds.Contains(r.GrandPrixDriver.GrandPrix.SeasonId))
            .GroupBy(r => new
            {
                r.GrandPrixDriver.GrandPrix.SeasonId,
                r.GrandPrixDriver.DriverId,
                r.GrandPrixDriver.Driver.Nickname,
                DriverSlug = r.GrandPrixDriver.Driver.Slug,
                r.GrandPrixDriver.TeamId,
                TeamName = r.GrandPrixDriver.Team.Name,
                TeamColor = r.GrandPrixDriver.Team.Color
            })
            .Select(g => new StandingRow
            {
                SeasonId = g.Key.SeasonId,
                DriverId = g.Key.DriverId,
                Nickname = g.Key.Nickname,
                DriverSlug = g.Key.DriverSlug,
                TeamId = g.Key.TeamId,
                TeamName = g.Key.TeamName,
                TeamColor = g.Key.TeamColor,
                Points = g.Sum(r => (int)(r.PointsGained ?? 0)),
                Wins = g.Count(r => r.SessionType == SessionType.Race && r.Position == 1)
            })
            .ToListAsync(ct);

        var standingsBySeason = standings.ToLookup(s => s.SeasonId);

        return seasons
            .Select(season =>
            {
                var seasonStats = stats[season.Id];
                var seasonStandings = standingsBySeason[season.Id];

                return new SeasonSummaryDto
                {
                    Season = season,
                    Status = SeasonStatusRules.FromRounds(seasonStats.RoundsTotal, seasonStats.RoundsCompleted),
                    ProgressPercent = SeasonStatusRules.ProgressPercent(seasonStats.RoundsTotal, seasonStats.RoundsCompleted),
                    RoundsTotal = seasonStats.RoundsTotal,
                    RoundsCompleted = seasonStats.RoundsCompleted,
                    DriverCount = seasonStats.DriverCount,
                    FirstRaceAt = seasonStats.FirstRaceAt,
                    LastRaceAt = seasonStats.LastRaceAt,
                    NextGrandPrix = seasonStats.NextGrandPrix,
                    DriverLeader = GetDriverLeader(seasonStandings),
                    TeamLeader = GetTeamLeader(seasonStandings)
                };
            })
            .ToList();
    }

    private static SeasonDriverStandingDto? GetDriverLeader(IEnumerable<StandingRow> standings)
    {
        return standings
            .GroupBy(s => s.DriverId)
            .Select(g => new SeasonDriverStandingDto
            {
                DriverId = g.Key,
                Nickname = g.First().Nickname,
                Slug = g.First().DriverSlug,
                Points = g.Sum(s => s.Points),
                Wins = g.Sum(s => s.Wins)
            })
            .Where(d => d.Points > 0)
            .OrderByDescending(d => d.Points)
            .ThenByDescending(d => d.Wins)
            .FirstOrDefault();
    }

    private static SeasonTeamStandingDto? GetTeamLeader(IEnumerable<StandingRow> standings)
    {
        return standings
            .GroupBy(s => s.TeamId)
            .Select(g => new SeasonTeamStandingDto
            {
                TeamId = g.Key,
                Name = g.First().TeamName,
                Color = g.First().TeamColor,
                Points = g.Sum(s => s.Points)
            })
            .Where(t => t.Points > 0)
            .OrderByDescending(t => t.Points)
            .FirstOrDefault();
    }

    private sealed class StandingRow
    {
        public int SeasonId { get; init; }
        public int DriverId { get; init; }
        public string Nickname { get; init; } = null!;
        public string DriverSlug { get; init; } = null!;
        public int TeamId { get; init; }
        public string TeamName { get; init; } = null!;
        public string? TeamColor { get; init; }
        public int Points { get; init; }
        public int Wins { get; init; }
    }
}
