namespace RacingLeagueHub.Application.Dtos.Auth.SSO;

public sealed record GoogleUserInfo(
    string ProviderUserId,
    string Email,
    bool EmailVerified,
    string? Name,
    string? PictureUrl
);