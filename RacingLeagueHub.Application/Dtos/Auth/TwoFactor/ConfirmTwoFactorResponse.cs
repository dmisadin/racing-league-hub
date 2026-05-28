namespace RacingLeagueHub.Application.Dtos.Auth.TwoFactor;

public record ConfirmTwoFactorResponse(
    IReadOnlyList<string> RecoveryCodes
);