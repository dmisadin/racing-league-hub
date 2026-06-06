using Google.Apis.Auth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using RacingLeagueHub.Application.Dtos.Auth.SSO;
using RacingLeagueHub.Application.Services.Abstractions;
using RacingLeagueHub.Infrastructure.Configuration;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace RacingLeagueHub.Infrastructure.Auth.SSO;

internal sealed class GoogleOAuthService : IGoogleOAuthService
{
    private readonly HttpClient httpClient;
    private readonly GoogleAuthOptions options;

    public GoogleOAuthService(
        HttpClient httpClient,
        IOptions<GoogleAuthOptions> options)
    {
        this.httpClient = httpClient;
        this.options = options.Value;
    }

    public string BuildAuthorizationUrl(string state)
    {
        var query = new Dictionary<string, string?>
        {
            ["client_id"] = options.ClientId,
            ["redirect_uri"] = options.RedirectUri,
            ["response_type"] = "code",
            ["scope"] = "openid email profile",
            ["state"] = state,
            ["access_type"] = "online",
            ["prompt"] = "select_account"
        };

        return QueryHelpers.AddQueryString(
            "https://accounts.google.com/o/oauth2/v2/auth",
            query);
    }

    public async Task<GoogleUserInfo> ExchangeCodeAsync(string code, CancellationToken ct = default)
    {
        var tokenResponse = await httpClient.PostAsync(
            "https://oauth2.googleapis.com/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["code"] = code,
                ["client_id"] = options.ClientId,
                ["client_secret"] = options.ClientSecret,
                ["redirect_uri"] = options.RedirectUri,
                ["grant_type"] = "authorization_code"
            }),
            ct);

        tokenResponse.EnsureSuccessStatusCode();

        var tokenPayload = await tokenResponse.Content
            .ReadFromJsonAsync<GoogleTokenResponse>(cancellationToken: ct);

        if (tokenPayload is null || string.IsNullOrWhiteSpace(tokenPayload.IdToken))
            throw new UnauthorizedAccessException("Google did not return an ID token.");

        var payload = await GoogleJsonWebSignature.ValidateAsync(
            tokenPayload.IdToken,
            new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = [options.ClientId]
            });

        return new GoogleUserInfo(
            ProviderUserId: payload.Subject,
            Email: payload.Email,
            EmailVerified: payload.EmailVerified,
            Name: payload.Name,
            PictureUrl: payload.Picture);
    }

    private sealed class GoogleTokenResponse
    {
        [JsonPropertyName("id_token")]
        public string? IdToken { get; set; }

        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }

        [JsonPropertyName("scope")]
        public string? Scope { get; set; }
    }
}