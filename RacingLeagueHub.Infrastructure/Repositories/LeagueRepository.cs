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

    public async Task<TDto?> GetBySlugAsync<TDto>(
        string leagueSlug,
        Expression<Func<League, TDto>> selector,
        CancellationToken ct = default)
    {
        return await Query()
            .AsNoTracking()
            .Where(l => l.Slug == leagueSlug)
            .Select(selector)
            .FirstOrDefaultAsync(ct);
    }
}
