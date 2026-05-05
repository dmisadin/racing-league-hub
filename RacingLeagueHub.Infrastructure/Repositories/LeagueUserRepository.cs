using Microsoft.EntityFrameworkCore;
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
        return await Query().Where(lu => lu.UserId == userId).ToListAsync();
    }
}