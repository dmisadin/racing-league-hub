using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Identity.Authentication.PasswordResetTokens;
using RacingLeagueHub.Domain.Entities.Authentication;

namespace RacingLeagueHub.Infrastructure.Persistence.Identity.Authentication.PasswordResetTokens;

internal class PasswordResetTokenCommands : IPasswordResetTokenCommands
{
    private readonly RacingContext context;

    public PasswordResetTokenCommands(RacingContext context)
    {
        this.context = context;
    }

    public async Task<PasswordResetToken> AddAsync(string token, long userId)
    {
        PasswordResetToken passwordResetToken = new PasswordResetToken
        {
            Token = token,
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
        };

        await context.PasswordResetToken.AddAsync(passwordResetToken);
        await context.SaveChangesAsync();

        return passwordResetToken;
    }

    public async Task InvalidateUserTokensAsync(long userId, CancellationToken ct = default)
    {
        await context.PasswordResetToken
            .Where(t => t.UserId == userId 
                && !t.IsUsed 
                && t.ExpiresAt > DateTime.UtcNow)
            .ExecuteUpdateAsync(s => s.SetProperty(t => t.IsUsed, true), ct);
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}