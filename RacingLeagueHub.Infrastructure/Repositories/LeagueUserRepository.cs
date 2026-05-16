using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Models.Enums;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal class LeagueUserRepository : GenericRepository<LeagueUser>, ILeagueUserRepository
{
    public LeagueUserRepository(AdventureContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<LeagueUser>> GetAllLeagueRolesForUser(long userId)
    {
        return await Query()
            .Where(lu => lu.UserId == userId)
            .Include(lu => lu.League)
            .ToListAsync();
    }

    public async Task<LeagueUser?> GetByLeagueAndUserAsync(
        long leagueId,
        long userId,
        CancellationToken cancellationToken = default)
    {
        return await Query()
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.LeagueId == leagueId && x.UserId == userId,
                cancellationToken);
    }
}