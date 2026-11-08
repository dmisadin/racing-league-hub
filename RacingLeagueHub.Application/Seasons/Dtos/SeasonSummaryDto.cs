using RacingLeagueHub.Domain.Common.Models;
using RacingLeagueHub.Domain.Common.Models.Enums;

namespace RacingLeagueHub.Application.Seasons.Dtos;

public class LeagueSeasonsOverviewDto
{
    public int SeasonCount { get; set; }
    public int RacesHeld { get; set; }
    public IReadOnlyList<SeasonSummaryDto> CurrentSeasons { get; set; } = [];
    public IReadOnlyList<SeasonSummaryDto> UpcomingSeasons { get; set; } = [];

    /// <summary>First page only; further pages come from the summaries endpoint filtered by <see cref="SeasonStatus.Completed"/>.</summary>
    public PagedResult<SeasonSummaryDto> CompletedSeasons { get; set; } = new([], 1, 10, 0);
}

public class LeagueSeasonStatsDto
{
    public int SeasonCount { get; set; }
    public int RacesHeld { get; set; }
}

public class SeasonSummaryDto
{
    public SeasonDto Season { get; set; }
    public SeasonStatus Status { get; set; }
    public int ProgressPercent { get; set; }
    public int RoundsTotal { get; set; }
    public int RoundsCompleted { get; set; }
    public int DriverCount { get; set; }
    public DateTimeOffset? FirstRaceAt { get; set; }
    public DateTimeOffset? LastRaceAt { get; set; }
    public SeasonNextGrandPrixDto? NextGrandPrix { get; set; }
    public SeasonDriverStandingDto? DriverLeader { get; set; }
    public SeasonTeamStandingDto? TeamLeader { get; set; }
}

public class SeasonNextGrandPrixDto
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public DateTimeOffset StartingAt { get; set; }
    public string TrackName { get; set; }
    public string? TrackCity { get; set; }
}

public class SeasonDriverStandingDto
{
    public int DriverId { get; set; }
    public string Nickname { get; set; }
    public string Slug { get; set; }
    public int Points { get; set; }
    public int Wins { get; set; }
}

public class SeasonTeamStandingDto
{
    public int TeamId { get; set; }
    public string Name { get; set; }
    public string? Color { get; set; }
    public int Points { get; set; }
}
