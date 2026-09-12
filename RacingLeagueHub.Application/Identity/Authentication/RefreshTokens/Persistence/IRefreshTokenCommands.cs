using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Identity.Authentication.RefreshTokens.Persistence;

public interface IRefreshTokenCommands
{
    Task<RefreshToken> AddAsync(string refreshToken, long userId, DateTime expiresAt, CancellationToken ct = default);
    Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
