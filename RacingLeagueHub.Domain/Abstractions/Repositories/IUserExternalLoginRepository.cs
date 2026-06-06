using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Domain.Abstractions.Repositories;

public interface IUserExternalLoginRepository : IRepository<UserExternalLogin>
{
    Task<UserExternalLogin?> FindByProviderAsync(string provider, string providerUserId, CancellationToken ct = default);
}