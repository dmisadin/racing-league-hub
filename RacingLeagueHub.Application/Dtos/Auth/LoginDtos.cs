namespace RacingLeagueHub.Application.Dtos.Auth;

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

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