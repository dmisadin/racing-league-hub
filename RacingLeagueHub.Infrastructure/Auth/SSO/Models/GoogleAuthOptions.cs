namespace RacingLeagueHub.Infrastructure.Auth.SSO.Models;

public sealed class GoogleAuthOptions
{
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string RedirectUri { get; set; } = null!;
    public string FrontendCallbackUrl { get; set; } = null!;
}