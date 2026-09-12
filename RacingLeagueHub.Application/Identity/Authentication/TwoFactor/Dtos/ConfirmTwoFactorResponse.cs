namespace RacingLeagueHub.Application.Identity.Authentication.TwoFactor.Dtos;

public record ConfirmTwoFactorResponse(
    IReadOnlyList<string> RecoveryCodes
);