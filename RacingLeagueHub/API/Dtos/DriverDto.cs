namespace RacingLeagueHub.API.Dtos;

public class DriverDto : BaseDto
{
    public string Nickname { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Country { get; set; }
    public string Slug { get; set; }
}
