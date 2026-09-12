using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes.Persistence;

public interface IUserRecoveryCodeCommands
{
    Task AddRangeAsync(IEnumerable<UserRecoveryCode> userRecoveryCodes, CancellationToken ct = default);
    Task DeleteForUserAsync(long userId, CancellationToken ct = default);
}
