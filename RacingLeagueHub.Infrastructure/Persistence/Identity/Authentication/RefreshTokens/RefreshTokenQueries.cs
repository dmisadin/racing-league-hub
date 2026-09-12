using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Identity.Authentication.Persistence;

namespace RacingLeagueHub.Infrastructure.Persistence.Identity.Authentication.RefreshTokens;

internal class RefreshTokenQueries : IRefreshTokenQueries
{
    private readonly RacingContext context;

    public RefreshTokenQueries(RacingContext context)
    {
        this.context = context;
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct = default)
    {
        return await context.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == token, ct);
    }

    public async Task<RefreshToken?> GetRefreshTokenWithUserAsync(string token, CancellationToken ct = default)
    {
        return await context.RefreshToken
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token, ct);
    }
}
