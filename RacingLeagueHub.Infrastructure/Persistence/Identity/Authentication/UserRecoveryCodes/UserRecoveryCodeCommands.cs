using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes.Persistence;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Infrastructure.Persistence;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal class UserRecoveryCodeCommands : IUserRecoveryCodeCommands
{
    private readonly RacingContext racingContext;

    public UserRecoveryCodeCommands(RacingContext racingContext)
    {
        this.racingContext = racingContext;
    }

    public Task AddRangeAsync(IEnumerable<UserRecoveryCode> userRecoveryCodes, CancellationToken ct = default)
    {
        return racingContext.AddRangeAsync(userRecoveryCodes, ct);
    }

    public Task DeleteForUserAsync(
        long userId,
        CancellationToken ct = default)
    {
        return racingContext.UserRecoveryCode
            .Where(x => x.UserId == userId)
            .ExecuteDeleteAsync(ct);
    }
}