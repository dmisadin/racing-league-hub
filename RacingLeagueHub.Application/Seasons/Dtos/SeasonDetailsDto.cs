using RacingLeagueHub.Application.Common.Dtos;
using RacingLeagueHub.Application.Divisions.Dtos;
using RacingLeagueHub.Domain.Common.Models.Enums;
using RacingLeagueHub.Domain.Common.Models.Enums.Assists;
using RacingLeagueHub.Domain.Common.Models.Enums.Lobby;

namespace RacingLeagueHub.Application.Seasons.Dtos;

public class SeasonDetailsDto
{
    public SeasonDto Season { get; set; }
    public IReadOnlyList<SeasonDivisionSummaryDto> Divisions { get; set; } = [];
}

public class SeasonDivisionSummaryDto
{
    public int SeasonDivisionId { get; set; }
    public DivisionDto Division { get; set; }
    public SeasonStatus Status { get; set; }
    public int ProgressPercent { get; set; }
    public int RoundsTotal { get; set; }
    public int RoundsCompleted { get; set; }
    public DateTimeOffset? FirstRaceAt { get; set; }
    public DateTimeOffset? LastRaceAt { get; set; }
    public SeasonNextGrandPrixDto? NextGrandPrix { get; set; }
    public SeasonDivisionAssistsDto? Assists { get; set; }
    public SeasonDivisionLobbySettingsDto? LobbySettings { get; set; }
}

public class SeasonDivisionRefDto
{
    public int SeasonDivisionId { get; set; }
    public int DivisionId { get; set; }
    public string DivisionSlug { get; set; }
}

public class SeasonDivisionLookupDto
{
    public int SeasonId { get; set; }
    public IReadOnlyList<SeasonDivisionRefDto> Divisions { get; set; } = [];
}

public class SeasonDivisionAssistsDto : BaseDto
{
    public int SeasonDivisionId { get; set; }
    public RacingLine RacingLine { get; set; }
    public Gearbox Gearbox { get; set; }
    public TractionControl TractionControl { get; set; }
    public bool Abs { get; set; }
}

public class SeasonDivisionLobbySettingsDto : BaseDto
{
    public int SeasonDivisionId { get; set; }
    public QualifyingType QualifyingType { get; set; }
    public RaceDistance RaceDistancePercentage { get; set; }
    public bool FormationLap { get; set; }
    public Weather Weather { get; set; }
    public CornerCuttingStringency CornerCutting { get; set; }
    public CarDamage CarDamage { get; set; }
    public CarDamageRate CarDamageRate { get; set; }
    public bool ParcFerme { get; set; }
    public bool EqualCarPerformance { get; set; }
    public SafetyCar SafetyCar { get; set; }
    public bool Collisions { get; set; }
    public bool Ghosting { get; set; }
    public RaceStart RaceStart { get; set; }
}
