namespace RacingLeagueHub.Application.Identity.Authentication.TwoFactor.Dtos;

public sealed class TwoFactorSetupDto
{
    public string ManualEntryKey { get; set; } = null!;
    public string OtpAuthUri { get; set; } = null!;
}