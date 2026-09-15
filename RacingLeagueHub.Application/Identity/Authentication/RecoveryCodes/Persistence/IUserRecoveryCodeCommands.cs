using RacingLeagueHub.Domain.Users;

namespace RacingLeagueHub.Application.Identity.Authentication.RecoveryCodes.Persistence;

public interface IUserRecoveryCodeCommands
{
    Task AddRangeAsync(IEnumerable<UserRecoveryCode> userRecoveryCodes, CancellationToken ct = default);
    Task DeleteForUserAsync(int userId, CancellationToken ct = default);
}
