namespace RacingLeagueHub.Application.Dtos.Auth.Totp;

public sealed class ConfirmTwoFactorDto
{
    public string Code { get; set; } = null!;
}