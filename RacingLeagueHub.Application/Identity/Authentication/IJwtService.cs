using RacingLeagueHub.Application.Users.Models;
using RacingLeagueHub.Domain.Users;
using System.Security.Claims;

namespace RacingLeagueHub.Application.Identity.Authentication;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    DateTime GetAccessTokenExpiry();


    string GenerateTwoFactorToken(User user);
    ClaimsPrincipal? ValidateTwoFactorToken(string token);
    int GetUserIdFromPrincipal(ClaimsPrincipal principal);
}