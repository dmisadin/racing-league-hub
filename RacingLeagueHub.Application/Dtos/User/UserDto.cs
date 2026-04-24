using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.Dtos.User;

public record UserDto
{
    public UserDto(long id, string email, string username, bool isAdmin, long? driverId)
    {
        this.Id = new EncryptedId(id);
        this.Email = email;
        this.Username = username;
        this.IsAdmin = isAdmin;
        this.DriverId = driverId.HasValue ? new EncryptedId(driverId.Value) : null;
    }

    public EncryptedId Id { get; init; }
    public string Email { get; init; }
    public string Username { get; init; }
    public bool IsAdmin { get; init; }
    public EncryptedId? DriverId { get; init; }
};