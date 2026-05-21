using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Models;
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
        return await Query()
            .Where(s => s.Slug == seasonSlug && s.League.Slug == leagueSlug)
            .Select(selector)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PagedResult<TDto>?> GetLeagueSeasonsAsync<TDto>(
        string leagueSlug, 
        Expression<Func<Season, TDto>> selector,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = Query().Where(s => s.League.Slug == leagueSlug);

        return await GetPagedAsync(selector, query, page, pageSize, ct);
    }

    public async Task<PagedResult<TDto>?> GetLeagueSeasonsAsync<TDto>(
        long leagueId, 
        Expression<Func<Season, TDto>> selector,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = Query().Where(s => s.LeagueId == leagueId);

        return await GetPagedAsync(selector, query, page, pageSize, ct);
    }
}
