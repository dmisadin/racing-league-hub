using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.Dtos.User;

public class UserDto : BaseDto
{
    public string Username { get; set; }
    public EncryptedId? DriverId { get; set; }
}
