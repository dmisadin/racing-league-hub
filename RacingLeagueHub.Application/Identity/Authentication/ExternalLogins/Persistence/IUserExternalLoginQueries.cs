using RacingLeagueHub.Domain.Users;

namespace RacingLeagueHub.Application.Identity.Authentication.ExternalLogins.Persistence;

public interface IUserExternalLoginQueries
{
    Task<UserExternalLogin?> FindByProviderAsync(string provider, string providerUserId, CancellationToken ct = default);
}