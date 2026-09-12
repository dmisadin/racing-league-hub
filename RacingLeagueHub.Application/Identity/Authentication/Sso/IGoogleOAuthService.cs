using RacingLeagueHub.Application.Identity.Authentication.Sso.Models;

namespace RacingLeagueHub.Application.Identity.Authentication.Sso;

public interface IGoogleOAuthService
{
    string BuildAuthorizationUrl(string state);

    Task<GoogleUserInfo> ExchangeCodeAsync(string code, CancellationToken ct = default);
}
