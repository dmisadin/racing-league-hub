using RacingLeagueHub.Application.Dtos.Auth.SSO;

namespace RacingLeagueHub.Application.Services.Abstractions;

public interface IGoogleOAuthService
{
    string BuildAuthorizationUrl(string state);

    Task<GoogleUserInfo> ExchangeCodeAsync(string code, CancellationToken ct = default);
}
