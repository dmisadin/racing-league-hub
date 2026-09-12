using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes.Persistence;

public interface IUserRecoveryCodeQueries
{
    Task<List<UserRecoveryCode>> GetUnusedForUserAsync(long userId, CancellationToken ct = default);
    Task<int> CountUnusedForUserAsync(long userId, CancellationToken ct = default);
}
