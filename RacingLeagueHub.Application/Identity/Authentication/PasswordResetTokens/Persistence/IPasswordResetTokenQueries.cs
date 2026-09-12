using RacingLeagueHub.Domain.Entities.Authentication;

namespace RacingLeagueHub.Application.Identity.Authentication.PasswordResetTokens;

public interface IPasswordResetTokenQueries
{
    Task<PasswordResetToken?> GetTokenWithUserAsync(string token, CancellationToken ct = default);
}