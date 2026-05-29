namespace RacingLeagueHub.Application.Dtos.Auth.Totp;

public sealed class DisableTwoFactorDto
{
    public string Password { get; set; } = null!;
    public string? Code { get; set; }
}