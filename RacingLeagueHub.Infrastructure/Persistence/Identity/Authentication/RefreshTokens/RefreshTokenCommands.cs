using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Identity.Authentication.RefreshTokens.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.Identity.Authentication.RefreshTokens;

internal class RefreshTokenCommands : IRefreshTokenCommands
{
    private readonly RacingContext context;

    public RefreshTokenCommands(RacingContext context)
    {
        this.context = context;
    }

    public async Task<RefreshToken> AddAsync(string refreshToken, long userId, DateTime expiresAt, CancellationToken ct = default)
    {
        RefreshToken token = new RefreshToken
        {

            Token = refreshToken,
            UserId = userId,
            ExpiresAt = expiresAt
        };

        await context.RefreshToken.AddAsync(token, ct);
        await context.SaveChangesAsync(ct);

        return token;
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken ct = default)
    {
        RefreshToken token = await context.RefreshToken.FirstOrDefaultAsync(x => x.Token == refreshToken, ct)
            ?? throw new UnauthorizedAccessException("Token not found.");

        token.IsRevoked = true;

        await context.SaveChangesAsync(ct);
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
