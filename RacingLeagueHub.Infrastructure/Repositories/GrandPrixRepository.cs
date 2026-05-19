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

    public async Task<TDto?> GetBySlugAsync<TDto>(
        string leagueSlug,
        string seasonSlug,
        string grandPrixSlug,
        Expression<Func<GrandPrix, TDto>> selector,
        CancellationToken ct = default)
    {
        return await Query()
            .AsNoTracking()
            .Where(x =>
                x.Slug == grandPrixSlug &&
                x.Season.Slug == seasonSlug &&
                x.Season.League.Slug == leagueSlug)
            .Select(selector)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PagedResult<TDto>> GetSeasonGrandsPrixAsync<TDto>(
        string leagueSlug,
        string seasonSlug,
        Expression<Func<GrandPrix, TDto>> selector,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = Query()
            .AsNoTracking()
            .Where(x =>
                x.Season.Slug == seasonSlug &&
                x.Season.League.Slug == leagueSlug);

        return await GetPagedAsync(selector, query, page, pageSize, ct);
    }

    public async Task<long?> UpdateBySlugAsync<TDto>(
        string leagueSlug,
        string seasonSlug,
        string grandPrixSlug,
        Func<GrandPrix, TDto, bool> mappingFunction,
        TDto dto,
        CancellationToken ct = default)
    {
        var entity = await Query()
            .FirstOrDefaultAsync(x =>
                x.Slug == grandPrixSlug &&
                x.Season.Slug == seasonSlug &&
                x.Season.League.Slug == leagueSlug,
                ct);

        if (entity is null)
            return null;

        var originalSeasonId = entity.SeasonId;

        var changed = mappingFunction(entity, dto);

        entity.SeasonId = originalSeasonId;

        if (!changed)
            return entity.Id;

        await CommitAsync(ct);

        return entity.Id;
    }

    public async Task<int> DeleteBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string grandPrixSlug,
        CancellationToken ct = default)
    {
        return await Query()
            .Where(x =>
                x.Slug == grandPrixSlug &&
                x.Season.Slug == seasonSlug &&
                x.Season.League.Slug == leagueSlug)
            .ExecuteDeleteAsync(ct);
    }
}
