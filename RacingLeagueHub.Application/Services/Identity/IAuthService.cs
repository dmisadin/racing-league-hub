using RacingLeagueHub.Application.Dtos.Auth;
using RacingLeagueHub.Application.Dtos.Auth.SSO;

namespace RacingLeagueHub.Application.Services.Identity;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct = default);
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);
    Task<AuthResponse> LoginWithTwoFactorAsync(TwoFactorLoginRequest req, CancellationToken ct = default);
    Task<AuthResponse> RefreshTokenAsync(CancellationToken ct = default);
    Task RevokeTokenAsync(CancellationToken ct = default);
    Task ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken ct = default);
    Task ResetPasswordAsync(ResetPasswordRequest request, CancellationToken ct = default);

    Task<AuthResponse> LoginWithGoogleAsync(GoogleUserInfo googleUser, CancellationToken ct = default);
}
