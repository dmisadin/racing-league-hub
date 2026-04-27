using RacingLeagueHub.Domain.Entities.Authentication;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Domain.Abstractions;

public interface IPasswordResetTokenRepository : IRepository<PasswordResetToken>
{
    Task<PasswordResetToken?> GetTokenWithUserAsync(string token, CancellationToken ct = default);
    Task InvalidateUserTokensAsync(long userId, CancellationToken ct = default);
}