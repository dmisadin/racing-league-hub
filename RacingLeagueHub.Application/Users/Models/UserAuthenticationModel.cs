namespace RacingLeagueHub.Application.Users.Models;

public class UserAuthenticationModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; }
    public int? DriverId { get; set; }

    public bool TwoFactorEnabled { get; set; }
    public string? TwoFactorSecret { get; set; }
    public int? LastTotpTimeStepUsed { get; set; }
}
