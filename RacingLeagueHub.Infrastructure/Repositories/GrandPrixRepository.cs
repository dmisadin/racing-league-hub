using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities.GrandsPrix;
using System.Linq.Expressions;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal class GrandPrixRepository : GenericRepository<GrandPrix>, IGrandPrixRepository
{
    public GrandPrixRepository(AdventureContext dbContext) : base(dbContext)
    {
    }

    public async Task<TDto?> GetBySlugAsync<TDto>(string leagueSlug, string seasonSlug, string grandPrixSlug, Expression<Func<GrandPrix, TDto>> selector, CancellationToken ct = default)
    {
        return await dbContext.Set<GrandPrix>()
            .Where(gp => gp.Slug == grandPrixSlug 
                        && gp.Season.Slug == seasonSlug 
                        && gp.Season.League.Slug == leagueSlug)
            .Select(selector)
            .FirstOrDefaultAsync();
    }

    public async Task<PagedResult<TDto>?> GetSeasonGrandsPrixAsync<TDto>(string leagueSlug, string seasonSlug, Expression<Func<GrandPrix, TDto>> selector, int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var query = this.dbContext.Set<GrandPrix>().Where(gp => gp.Season.League.Slug == leagueSlug && gp.Season.Slug == seasonSlug);
        return await GetPagedAsync(selector, query, page, pageSize, ct);
    }

    public async Task<PagedResult<TDto>?> GetSeasonGrandsPrixAsync<TDto>(long seasonId, Expression<Func<GrandPrix, TDto>> selector, int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var query = this.dbContext.Set<GrandPrix>().Where(gp => gp.SeasonId == seasonId);
        return await GetPagedAsync(selector, query, page, pageSize, ct);
    }
}
