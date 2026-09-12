namespace RacingLeagueHub.Application.Identity.Authentication.Sso.Models;

public sealed record GoogleUserInfo(
    string ProviderUserId,
    string Email,
    bool EmailVerified,
    string? Name,
    string? PictureUrl
);