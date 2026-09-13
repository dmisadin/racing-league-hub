using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Identity.Authentication.ExternalLogins.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.Identity.Authentication.UserExternalLogins;

internal sealed class UserExternalLoginQueries : IUserExternalLoginQueries
{
    private readonly RacingContext racingContext;

    public UserExternalLoginQueries(RacingContext racingContext)
    {
        this.racingContext = racingContext;
    }

    public Task<UserExternalLogin?> FindByProviderAsync(
        string provider,
        string providerUserId,
        CancellationToken ct = default)
    {
        return racingContext.UserExternalLogin
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => 
                x.Provider == provider
                && x.ProviderUserId == providerUserId, ct);
    }
}