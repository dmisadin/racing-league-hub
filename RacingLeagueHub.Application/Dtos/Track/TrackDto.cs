using RacingLeagueHub.Domain.Models.Constants;

namespace RacingLeagueHub.Application.Dtos.Track;

public class TrackDto : BaseDto
{
    public string Name { get; set; }
    public string CountryAlpha2 { get; set; }
    public Country? Country { get; set; }
    public string City { get; set; }
    public decimal? Elevation { get; set; }
    public string? ShortName { get; set; }

    public List<TrackLayoutDto>? TrackLayouts { get; set; }
}
