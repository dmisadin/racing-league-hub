using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.Divisions.Dtos;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Application.Seasons.Persistence;
using RacingLeagueHub.Domain.Common.Models;
using RacingLeagueHub.Domain.Common.Models.Enums;
using RacingLeagueHub.Domain.Leagues.Divisions;
using RacingLeagueHub.Domain.Leagues.Seasons;
using RacingLeagueHub.Domain.Leagues.Seasons.Rules;
using RacingLeagueHub.Infrastructure.Extensions;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons;

internal class SeasonQueries : ISeasonQueries
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<Season, SeasonDto> mapper;
    private readonly IDtoMapper<SeasonDivisionAssists, SeasonDivisionAssistsDto> assistsMapper;
    private readonly IDtoMapper<SeasonDivisionLobbySettings, SeasonDivisionLobbySettingsDto> lobbySettingsMapper;
    private readonly IDtoMapper<Division, DivisionDto> divisionMapper;

    public SeasonQueries(RacingContext racingContext,
        IDtoMapper<Season, SeasonDto> mapper,
        IDtoMapper<SeasonDivisionAssists, SeasonDivisionAssistsDto> assistsMapper,
        IDtoMapper<SeasonDivisionLobbySettings, SeasonDivisionLobbySettingsDto> lobbySettingsMapper,
        IDtoMapper<Division, DivisionDto> divisionMapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
        this.assistsMapper = assistsMapper;
        this.lobbySettingsMapper = lobbySettingsMapper;
        this.divisionMapper = divisionMapper;
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
            .OrderBy(s => s.SeasonDivisions.SelectMany(sd => sd.GrandsPrix).Min(gp => (DateTimeOffset?)gp.StartingAt))
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
                    .SelectMany(s => s.SeasonDivisions)
                    .SelectMany(sd => sd.GrandsPrix)
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

    public async Task<SeasonDetailsDto?> GetDetailsAsync(
        string leagueSlug,
        string seasonSlug,
        CancellationToken ct = default)
    {
        return await racingContext.Season
            .AsNoTracking()
            .Where(s => s.Slug == seasonSlug && s.League.Slug == leagueSlug)
            .Select(mapper.ToDtoExpression())
            .Select(season => new SeasonDetailsDto { Season = season })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IReadOnlyList<SeasonDivisionSummaryDto>> GetDivisionSummariesAsync(
        int seasonId,
        CancellationToken ct = default)
    {
        var now = DateTimeOffset.UtcNow;

        var divisions = await racingContext.SeasonDivision
            .AsNoTracking()
            .Where(sd => sd.SeasonId == seasonId)
            .OrderBy(sd => sd.Division.Tier)
            .ThenBy(sd => sd.Division.Name)
            .Select(sd => sd.Division)
            .Select(divisionMapper.ToDtoExpression())
            .ToListAsync(ct);

        if (divisions.Count == 0)
            return [];

        // Every division runs its own calendar: stats only count the division's own Grands Prix.
        var stats = await racingContext.SeasonDivision
            .AsNoTracking()
            .Where(sd => sd.SeasonId == seasonId)
            .Select(sd => new
            {
                sd.Id,
                sd.DivisionId,
                RoundsTotal = sd.GrandsPrix.Count(),
                RoundsCompleted = sd.GrandsPrix.Count(gp => gp.StartingAt <= now),
                FirstRaceAt = sd.GrandsPrix.Min(gp => (DateTimeOffset?)gp.StartingAt),
                LastRaceAt = sd.GrandsPrix.Max(gp => (DateTimeOffset?)gp.StartingAt),
                NextGrandPrix = sd.GrandsPrix
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
                    .FirstOrDefault(),
                Assists = racingContext.SeasonDivisionAssists
                    .Where(a => a.SeasonDivisionId == sd.Id)
                    .OrderByDescending(a => a.Id)
                    .Select(assistsMapper.ToDtoExpression())
                    .FirstOrDefault(),
                LobbySettings = racingContext.SeasonDivisionLobbySettings
                    .Where(l => l.SeasonDivisionId == sd.Id)
                    .OrderByDescending(l => l.Id)
                    .Select(lobbySettingsMapper.ToDtoExpression())
                    .FirstOrDefault()
            })
            .ToDictionaryAsync(x => x.DivisionId, ct);

        return divisions
            .Select(division =>
            {
                var s = stats[division.Id];

                return new SeasonDivisionSummaryDto
                {
                    SeasonDivisionId = s.Id,
                    Division = division,
                    Status = SeasonStatusRules.FromRounds(s.RoundsTotal, s.RoundsCompleted),
                    ProgressPercent = SeasonStatusRules.ProgressPercent(s.RoundsTotal, s.RoundsCompleted),
                    RoundsTotal = s.RoundsTotal,
                    RoundsCompleted = s.RoundsCompleted,
                    FirstRaceAt = s.FirstRaceAt,
                    LastRaceAt = s.LastRaceAt,
                    NextGrandPrix = s.NextGrandPrix,
                    Assists = s.Assists,
                    LobbySettings = s.LobbySettings
                };
            })
            .ToList();
    }

    public async Task<SeasonDivisionLookupDto?> GetDivisionLookupAsync(
        string leagueSlug,
        string seasonSlug,
        CancellationToken ct = default)
    {
        // The nested collection projection is translated into a single SQL statement (LEFT JOIN).
        return await racingContext.Season
            .AsNoTracking()
            .Where(s => s.Slug == seasonSlug && s.League.Slug == leagueSlug)
            .Select(s => new SeasonDivisionLookupDto
            {
                SeasonId = s.Id,
                Divisions = s.SeasonDivisions
                    .Select(sd => new SeasonDivisionRefDto
                    {
                        SeasonDivisionId = sd.Id,
                        DivisionId = sd.DivisionId,
                        DivisionSlug = sd.Division.Slug
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(ct);
    }

    private static IQueryable<Season> OrderForStatus(IQueryable<Season> query, SeasonStatus? status)
    {
        return status switch
        {
            SeasonStatus.Completed => query
                .OrderByDescending(s => s.SeasonDivisions.SelectMany(sd => sd.GrandsPrix).Max(gp => (DateTimeOffset?)gp.StartingAt))
                .ThenByDescending(s => s.Id),
            SeasonStatus.Upcoming => query
                .OrderBy(s => s.SeasonDivisions.SelectMany(sd => sd.GrandsPrix).Min(gp => (DateTimeOffset?)gp.StartingAt))
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

        // Season totals span all divisions; a driver entered in several divisions counts once.
        var stats = await racingContext.Season
            .AsNoTracking()
            .Where(s => seasonIds.Contains(s.Id))
            .Select(s => new
            {
                s.Id,
                RoundsTotal = s.SeasonDivisions.SelectMany(sd => sd.GrandsPrix).Count(),
                RoundsCompleted = s.SeasonDivisions.SelectMany(sd => sd.GrandsPrix).Count(gp => gp.StartingAt <= now),
                DriverCount = s.SeasonDivisions.SelectMany(sd => sd.Drivers).Select(d => d.DriverId).Distinct().Count(),
                FirstRaceAt = s.SeasonDivisions.SelectMany(sd => sd.GrandsPrix).Min(gp => (DateTimeOffset?)gp.StartingAt),
                LastRaceAt = s.SeasonDivisions.SelectMany(sd => sd.GrandsPrix).Max(gp => (DateTimeOffset?)gp.StartingAt),
                NextGrandPrix = s.SeasonDivisions.SelectMany(sd => sd.GrandsPrix)
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
            .Where(r => seasonIds.Contains(r.GrandPrixDriver.GrandPrix.SeasonDivision.SeasonId))
            .GroupBy(r => new
            {
                r.GrandPrixDriver.GrandPrix.SeasonDivision.SeasonId,
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
