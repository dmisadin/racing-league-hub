using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Abstractions.Repositories;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal class UserRecoveryCodeRepository : GenericRepository<UserRecoveryCode>, IUserRecoveryCodeRepository
{
    public UserRecoveryCodeRepository(AdventureContext db) : base(db)
    {
    }

    public Task<List<UserRecoveryCode>> GetUnusedForUserAsync(
        long userId,
        CancellationToken ct = default)
    {
        return Query()
            .Where(x => x.UserId == userId && x.UsedAt == null)
            .ToListAsync(ct);
    }

    public Task DeleteForUserAsync(
        long userId,
        CancellationToken ct = default)
    {
        return Query()
            .Where(x => x.UserId == userId)
            .ExecuteDeleteAsync(ct);
    }

    public Task<int> CountUnusedForUserAsync(
        long userId,
        CancellationToken ct = default)
    {
        return Query().CountAsync(x => x.UserId == userId && x.UsedAt == null, ct);
    }
}