using RacingLeagueHub.API.Dtos;
using RacingLeagueHub.BLL.Models;

namespace RacingLeagueHub.API.Dtos.User;

public class UserDto : BaseDto
{
    public string Username { get; set; }
    public EncryptedId? DriverId { get; set; }
}
