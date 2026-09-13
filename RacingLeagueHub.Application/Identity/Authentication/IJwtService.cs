using RacingLeagueHub.Application.Users.Models;
using RacingLeagueHub.Domain.Entities;
using System.Security.Claims;

namespace RacingLeagueHub.Application.Identity.Authentication;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    DateTime GetAccessTokenExpiry();


    string GenerateTwoFactorToken(User user);
    ClaimsPrincipal? ValidateTwoFactorToken(string token);
    long GetUserIdFromPrincipal(ClaimsPrincipal principal);
}