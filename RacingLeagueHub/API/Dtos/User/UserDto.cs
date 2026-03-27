using RacingLeagueHub.API.Dtos;

namespace RacingLeagueHub.API.Dtos.User;

public class UserDto : BaseDto
{
    public string Username { get; set; }
    public long? DriverId { get; set; }
}
