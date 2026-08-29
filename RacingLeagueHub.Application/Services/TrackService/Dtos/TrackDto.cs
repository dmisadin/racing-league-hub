using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Dtos.Track;
using RacingLeagueHub.Domain.Models.Constants;

namespace RacingLeagueHub.Application.Services.TrackService.Dtos;

public class TrackDto : BaseDto
{
    public string Name { get; init; }
    public string CountryAlpha2 { get; init; }
    public Country? Country { get; init; }
    public string City { get; init; }
    public decimal? Elevation { get; init; }
    public string? ShortName { get; init; }

    public List<TrackLayoutDto>? TrackLayouts { get; init; }
}

public sealed class CreateTrackDto
{
    public string Name { get; init; }
    public string CountryAlpha2 { get; init; }
    public string City { get; init; }
    public decimal? Elevation { get; init; }
    public string? ShortName { get; init; }
}

public sealed class UpdateTrackDto
{
    public string Name { get; init; }
    public string CountryAlpha2 { get; init; }
    public string City { get; init; }
    public decimal? Elevation { get; init; }
    public string? ShortName { get; init; }
}