using RacingLeagueHub.Domain.Users;

namespace RacingLeagueHub.Application.Identity.Authentication.PasswordResetTokens;

public interface IPasswordResetTokenCommands
{
    Task<PasswordResetToken> AddAsync(string token, int userId);
    Task InvalidateUserTokensAsync(int userId, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}