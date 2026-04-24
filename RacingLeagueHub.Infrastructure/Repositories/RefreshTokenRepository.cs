using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AdventureContext dbContext) : base(dbContext)
    {
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct = default)
    {
        return await Query().Include(rt => rt.User)
                            .FirstOrDefaultAsync(rt => rt.Token == token, ct);
    }

    public async Task<RefreshToken?> GetRefreshTokenWithUserAsync(string token, CancellationToken ct = default)
    {
        return await Query().FirstOrDefaultAsync(rt => rt.Token == token, ct);
    }
}
