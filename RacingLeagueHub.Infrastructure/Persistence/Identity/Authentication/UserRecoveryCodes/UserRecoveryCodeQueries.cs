using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.Identity.Authentication.UserRecoveryCodes;

internal class UserRecoveryCodeQueries : IUserRecoveryCodeQueries
{
    private readonly RacingContext racingContext;

    public UserRecoveryCodeQueries(RacingContext racingContext)
    {
        this.racingContext = racingContext;
    }

    public Task<List<UserRecoveryCode>> GetUnusedForUserAsync(long userId, CancellationToken ct = default)
    {
        return racingContext.UserRecoveryCode
            .Where(x => x.UserId == userId && x.UsedAt == null)
            .ToListAsync(ct);
    }

    public Task DeleteForUserAsync(long userId, CancellationToken ct = default)
    {
        return racingContext.UserRecoveryCode
            .Where(x => x.UserId == userId)
            .ExecuteDeleteAsync(ct);
    }

    public Task<int> CountUnusedForUserAsync(long userId, CancellationToken ct = default)
    {
        return racingContext.UserRecoveryCode
            .CountAsync(x => x.UserId == userId 
            && x.UsedAt == null, ct);
    }
}