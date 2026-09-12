using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Identity.Authentication.PasswordResetTokens;
using RacingLeagueHub.Domain.Entities.Authentication;

namespace RacingLeagueHub.Infrastructure.Persistence.Identity.Authentication.PasswordResetTokens;

internal class PasswordResetTokenQueries : IPasswordResetTokenQueries
{
    private readonly RacingContext context;

    public PasswordResetTokenQueries(RacingContext context)
    {
        this.context = context;
    }


    public async Task<PasswordResetToken?> GetTokenWithUserAsync(string token, CancellationToken ct = default)
    {
        return await context.PasswordResetToken
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == token, ct);
    }
}