using RacingLeagueHub.Domain.Users;

namespace RacingLeagueHub.Application.Identity.Authentication.RefreshTokens.Persistence;

public interface IRefreshTokenCommands
{
    Task<RefreshToken> AddAsync(string refreshToken, int userId, DateTime expiresAt, CancellationToken ct = default);
    Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
