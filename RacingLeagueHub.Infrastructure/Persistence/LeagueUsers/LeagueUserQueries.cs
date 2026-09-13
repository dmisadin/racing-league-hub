using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.LeagueUsers.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.LeagueUsers;

internal class LeagueUserQueries : ILeagueUserQueries
{
    private readonly RacingContext racingContext;

    public LeagueUserQueries(RacingContext racingContext)
    {
        this.racingContext = racingContext;
    }

    public async Task<List<LeagueUser>> GetAllLeagueRolesForUserAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await racingContext.LeagueUser
            .Where(lu => lu.UserId == userId)
            .Include(lu => lu.League)
            .ToListAsync();
    }

    public async Task<LeagueUser?> GetByLeagueAndUserAsync(long leagueId, long userId, CancellationToken cancellationToken = default)
    {
        return await racingContext.LeagueUser
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.LeagueId == leagueId && x.UserId == userId,
                cancellationToken);
    }

    public async Task<LeagueUser?> GetByLeagueAndUserAsync(string leagueSlug, long userId, CancellationToken cancellationToken = default)
    {
        return await racingContext.LeagueUser
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.UserId == userId && x.League.Slug == leagueSlug,
                cancellationToken);
    }
}