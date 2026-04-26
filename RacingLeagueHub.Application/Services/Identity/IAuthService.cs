using RacingLeagueHub.Application.Dtos.Auth;

namespace RacingLeagueHub.Application.Services.Identity;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct = default);
    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);
    Task<AuthResponse> RefreshTokenAsync(CancellationToken ct = default);
    Task RevokeTokenAsync(CancellationToken ct = default);
}
