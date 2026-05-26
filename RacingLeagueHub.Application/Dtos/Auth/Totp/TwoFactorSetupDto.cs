namespace RacingLeagueHub.Application.Dtos.Auth.Totp;

public sealed class TwoFactorSetupDto
{
    public string ManualEntryKey { get; set; } = null!;
    public string OtpAuthUri { get; set; } = null!;
}