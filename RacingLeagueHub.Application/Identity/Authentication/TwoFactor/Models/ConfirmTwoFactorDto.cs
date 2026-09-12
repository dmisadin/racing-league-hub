namespace RacingLeagueHub.Application.Identity.Authentication.TwoFactor.Models;

public sealed class ConfirmTwoFactorDto
{
    public string Code { get; set; } = null!;
}