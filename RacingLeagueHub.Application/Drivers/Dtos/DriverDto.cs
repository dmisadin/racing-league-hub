using RacingLeagueHub.Application.Dtos;

namespace RacingLeagueHub.Application.Drivers.Dtos;

public class DriverDto : BaseDto
{
    public string Nickname { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public CountryDto? Country { get; set; }
    public string Slug { get; set; }
}
