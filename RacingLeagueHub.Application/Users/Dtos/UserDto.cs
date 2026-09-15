namespace RacingLeagueHub.Application.Users.Dtos;

public record UserDto
{
    public UserDto(int id, string email, string username, bool isAdmin, int? driverId)
    {
        this.Id = id;
        this.Email = email;
        this.Username = username;
        this.IsAdmin = isAdmin;
        this.DriverId = driverId;
    }

    public int Id { get; init; }
    public string Email { get; init; }
    public string Username { get; init; }
    public bool IsAdmin { get; init; }
    public int? DriverId { get; init; }
};

public class CreateUserDto
{
    public int Id { get; init; }
    public string Email { get; init; }
    public string Username { get; init; }
    public string? Password { get; init; }
    public bool IsAdmin { get; init; }
    public int? DriverId { get; init; }
}