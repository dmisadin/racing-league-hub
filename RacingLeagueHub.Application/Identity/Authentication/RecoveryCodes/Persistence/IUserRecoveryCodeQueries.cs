using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes.Persistence;

public interface IUserRecoveryCodeQueries
{
    Task<List<UserRecoveryCode>> GetUnusedForUserAsync(int userId, CancellationToken ct = default);
    Task<int> CountUnusedForUserAsync(int userId, CancellationToken ct = default);
}
