using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using RacingLeagueHub.Application.Services.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace RacingLeagueHub.Infrastructure.Auth.SSO;

internal sealed class SsoStateService : ISsoStateService
{
    private const string CookieName = "__Host-google_sso_state";
    private readonly IHttpContextAccessor httpContextAccessor;

    public SsoStateService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public string GenerateState()
    {
        return WebEncoders.Base64UrlEncode(RandomNumberGenerator.GetBytes(32));
    }

    public void SetStateCookie(string state)
    {
        var httpContext = httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("No HTTP context.");

        httpContext.Response.Cookies.Append(
            CookieName,
            state,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Path = "/",
                MaxAge = TimeSpan.FromMinutes(10)
            });
    }

    public bool ValidateAndClearState(string? returnedState)
    {
        var httpContext = httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("No HTTP context.");

        var valid =
            !string.IsNullOrWhiteSpace(returnedState) &&
            httpContext.Request.Cookies.TryGetValue(CookieName, out var storedState) &&
            CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(storedState),
                Encoding.UTF8.GetBytes(returnedState));

        httpContext.Response.Cookies.Delete(
            CookieName,
            new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Path = "/"
            });

        return valid;
    }
}