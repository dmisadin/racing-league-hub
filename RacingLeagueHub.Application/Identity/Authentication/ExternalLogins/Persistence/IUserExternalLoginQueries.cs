using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Identity.Authentication.ExternalLogins.Persistence;

public interface IUserExternalLoginQueries
{
    Task<UserExternalLogin?> FindByProviderAsync(string provider, string providerUserId, CancellationToken ct = default);
}