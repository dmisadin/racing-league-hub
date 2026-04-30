using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Domain.Abstractions;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct = default);
    Task<RefreshToken?> GetRefreshTokenWithUserAsync(string token, CancellationToken ct = default); 
}
