using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Services.Identity;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    DateTime GetAccessTokenExpiry();
}