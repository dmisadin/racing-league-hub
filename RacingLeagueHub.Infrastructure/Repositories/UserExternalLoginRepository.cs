using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Abstractions.Repositories;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal sealed class UserExternalLoginRepository
    : GenericRepository<UserExternalLogin>, IUserExternalLoginRepository
{

    public UserExternalLoginRepository(AdventureContext db) : base(db)
    {
    }

    public Task<UserExternalLogin?> FindByProviderAsync(
        string provider,
        string providerUserId,
        CancellationToken ct = default)
    {
        return Query()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Provider == provider
                                    && x.ProviderUserId == providerUserId, ct);
    }
}