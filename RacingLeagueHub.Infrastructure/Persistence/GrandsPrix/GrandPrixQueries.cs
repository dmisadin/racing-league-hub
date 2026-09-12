using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Application.GrandsPrix.Persistence;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities.GrandsPrix;
using RacingLeagueHub.Infrastructure.Extensions;

namespace RacingLeagueHub.Infrastructure.Persistence.GrandsPrix;

internal class GrandPrixQueries : IGrandPrixQueries
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<GrandPrix, GrandPrixDto> mapper;

    public GrandPrixQueries(RacingContext racingContext,
        IDtoMapper<GrandPrix, GrandPrixDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<GrandPrixDto?> GetBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string grandPrixSlug,
        CancellationToken ct = default)
    {
        return await racingContext.GrandPrix
            .AsNoTracking()
            .Where(x =>
                x.Slug == grandPrixSlug &&
                x.Season.Slug == seasonSlug &&
                x.Season.League.Slug == leagueSlug)
            .Select(mapper.ToDtoExpression())
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PagedResult<GrandPrixDto>> GetSeasonGrandsPrixAsync(
        string leagueSlug,
        string seasonSlug,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        return await racingContext.GrandPrix
            .AsNoTracking()
            .Where(x =>
                x.Season.Slug == seasonSlug &&
                x.Season.League.Slug == leagueSlug)
            .Select(mapper.ToDtoExpression())
            .ToPagedResultAsync(page, pageSize, ct);
    }
}
