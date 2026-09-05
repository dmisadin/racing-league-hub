using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.TrackLayouts.Dtos;

namespace RacingLeagueHub.Application.Services.TrackService.Dtos;

public class TrackDto : BaseDto
{
    public string Name { get; init; }
    public int CountryId { get; init; }
    public CountryDto? Country { get; init; }
    public string City { get; init; }
    public decimal? Elevation { get; init; }
    public string? ShortName { get; init; }

    public List<TrackLayoutDto>? TrackLayouts { get; init; }
}

public sealed class CreateTrackDto
{
    public string Name { get; init; }
    public int CountryId { get; init; }
    public string City { get; init; }
    public decimal? Elevation { get; init; }
    public string? ShortName { get; init; }
}

public sealed class UpdateTrackDto
{
    public string Name { get; init; }
    public int CountryId { get; init; }
    public string City { get; init; }
    public decimal? Elevation { get; init; }
    public string? ShortName { get; init; }
}