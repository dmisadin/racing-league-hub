namespace RacingLeagueHub.Application.Identity.Authentication.TwoFactor.Dtos;

public record RecoveryCodesResponse(
    IReadOnlyList<string> RecoveryCodes
);