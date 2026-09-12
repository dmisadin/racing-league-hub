using RacingLeagueHub.Application.Identity.Authentication.ExternalLogins.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.Identity.Authentication.UserExternalLogins;

internal class UserExternalLoginCommands : IUserExternalLoginCommands
{
    private readonly RacingContext racingContext;

    public UserExternalLoginCommands(RacingContext racingContext)
    {
        this.racingContext = racingContext;
    }

    public async Task AddAsync(UserExternalLogin externalLogin, CancellationToken ct = default)
    {
        await racingContext.UserExternalLogin.AddAsync(externalLogin, ct);
        await racingContext.SaveChangesAsync(ct);
    }
}
