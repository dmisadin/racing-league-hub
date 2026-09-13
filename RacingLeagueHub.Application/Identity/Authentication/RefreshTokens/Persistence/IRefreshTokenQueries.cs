using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Identity.Authentication.Persistence;

public interface IRefreshTokenQueries
{
    Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct = default);
    Task<RefreshToken?> GetRefreshTokenWithUserAsync(string token, CancellationToken ct = default); 
}
