namespace RacingLeagueHub.Application.Users.Models;

public class UserAuthenticationModel
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; }
    public long? DriverId { get; set; }

    public bool TwoFactorEnabled { get; set; }
    public string? TwoFactorSecret { get; set; }
    public long? LastTotpTimeStepUsed { get; set; }
}
