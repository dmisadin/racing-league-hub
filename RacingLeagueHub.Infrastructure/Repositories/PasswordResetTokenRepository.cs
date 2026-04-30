using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities.Authentication;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal class PasswordResetTokenRepository : GenericRepository<PasswordResetToken>, IPasswordResetTokenRepository
{
    public PasswordResetTokenRepository(AdventureContext dbContext) : base(dbContext)
    {
    }

    public async Task<PasswordResetToken?> GetTokenWithUserAsync(string token, CancellationToken ct = default)
    {
        return await Query()
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == token, ct);
    }

    public async Task InvalidateUserTokensAsync(long userId, CancellationToken ct = default)
    {
        await dbContext.Set<PasswordResetToken>()
            .Where(t => t.UserId == userId && !t.IsUsed && t.ExpiresAt > DateTime.UtcNow)
            .ExecuteUpdateAsync(s => s.SetProperty(t => t.IsUsed, true), ct);
    }
}