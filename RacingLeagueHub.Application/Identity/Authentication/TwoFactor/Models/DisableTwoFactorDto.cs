namespace RacingLeagueHub.Application.Identity.Authentication.TwoFactor.Models;

public sealed class DisableTwoFactorDto
{
    public string Password { get; set; } = null!;
    public string? Code { get; set; }
}