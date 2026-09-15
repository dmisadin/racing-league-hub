using RacingLeagueHub.Domain.Users;

namespace RacingLeagueHub.Application.Identity.Authentication.PasswordResetTokens;

public interface IPasswordResetTokenQueries
{
    Task<PasswordResetToken?> GetTokenWithUserAsync(string token, CancellationToken ct = default);
}