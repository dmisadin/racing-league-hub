namespace RacingLeagueHub.Application.Identity.Authentication.Dtos;

public record LoginResponse(
    bool RequiresTwoFactor,
    AuthResponse? Auth,
    string? TwoFactorToken
);

public record TwoFactorLoginRequest(
    string TwoFactorToken,
    string Code,
    bool RememberMe,
    bool IsRecoveryCode = false
);