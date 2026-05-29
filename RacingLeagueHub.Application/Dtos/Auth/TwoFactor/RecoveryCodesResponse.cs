namespace RacingLeagueHub.Application.Dtos.Auth.TwoFactor;

public record RecoveryCodesResponse(
    IReadOnlyList<string> RecoveryCodes
);