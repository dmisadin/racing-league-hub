using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal class LeagueRepository : GenericRepository<League>, ILeagueRepository
{
    public LeagueRepository(AdventureContext dbContext) : base(dbContext)
    {
    }

    public async Task<TDto?> GetBySlugAsync<TDto>(string slug, Expression<Func<League, TDto>> selector)
    {
        return await this.dbContext.Set<League>()
                        .Where(l => l.Slug == slug)
                        .Select(selector)
                        .FirstOrDefaultAsync();
    }
}
