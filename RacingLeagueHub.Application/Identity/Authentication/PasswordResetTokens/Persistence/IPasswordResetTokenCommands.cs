using RacingLeagueHub.Domain.Entities.Authentication;

namespace RacingLeagueHub.Application.Identity.Authentication.PasswordResetTokens;

public interface IPasswordResetTokenCommands
{
    Task<PasswordResetToken> AddAsync(string token, long userId);
    Task InvalidateUserTokensAsync(long userId, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}