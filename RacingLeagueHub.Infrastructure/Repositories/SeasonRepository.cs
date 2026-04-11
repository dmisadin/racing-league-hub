using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities.Seasons;
using RacingLeagueHub.Infrastructure;
using RacingLeagueHub.Infrastructure.Repositories;
using System.Linq.Expressions;

namespace RacingSeasonHub.Infrastructure.Repositories;

internal class SeasonRepository : GenericRepository<Season>, ISeasonRepository
{
    public SeasonRepository(AdventureContext dbContext) : base(dbContext)
    {
    }

    public async Task<TDto?> GetBySlugAsync<TDto>(
        string leagueSlug,
        string seasonSlug,
        Expression<Func<Season, TDto>> selector,
        CancellationToken ct = default)
    {
        return await dbContext.Set<Season>()
            .Where(s => s.Slug == seasonSlug && s.League.Slug == leagueSlug)
            .Select(selector)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IList<TDto>?> GetLeagueSeasonsAsync<TDto>(
        string leagueSlug, 
        Expression<Func<Season, TDto>> selector, 
        CancellationToken ct = default)
    {
        return await this.dbContext.Set<Season>()
                        .Where(s => s.League.Slug == leagueSlug)
                        .Select(selector)
                        .ToListAsync(ct);
    }
}
