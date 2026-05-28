using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Domain.Abstractions.Repositories;

public interface IUserRecoveryCodeRepository : IRepository<UserRecoveryCode>
{
    Task<List<UserRecoveryCode>> GetUnusedForUserAsync(long userId, CancellationToken ct = default);
    Task DeleteForUserAsync(long userId, CancellationToken ct = default);
    Task<int> CountUnusedForUserAsync(long userId, CancellationToken ct = default);
}
