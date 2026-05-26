using RacingLeagueHub.Domain.Entities;
using System.Security.Claims;

namespace RacingLeagueHub.Application.Services.Identity;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    DateTime GetAccessTokenExpiry();


    string GenerateTwoFactorToken(User user);
    ClaimsPrincipal? ValidateTwoFactorToken(string token);
    long GetUserIdFromPrincipal(ClaimsPrincipal principal);
}